using UnityEngine;

namespace Input
{
    public class InputRouter : MonoBehaviour
    {
        [SerializeField] private InputReader InputReader;
        [SerializeField] private Camera Camera;
        
        [Header("Layers")]
        [SerializeField] private LayerMask UILayerMask;
        [SerializeField] private LayerMask BoardLayerMask;

        [Header("Raycast")]
        [SerializeField] private float MaxDistance = 1000f;
        private readonly QueryTriggerInteraction _triggers = QueryTriggerInteraction.Ignore;

        private readonly RaycastHit[] _hits = new RaycastHit[32];

        private IInputReceiver _pressedReceiver;
        private bool _pressedIsUI;

        private void Awake()
        {
            if (Camera == null)
                Camera = Camera.main;
        }

        private void OnEnable()
        {
            InputReader.PointerDown += OnPointerDown;
            InputReader.PointerUp += OnPointerUp;
        }

        private void OnDisable()
        {
            InputReader.PointerDown -= OnPointerDown;
            InputReader.PointerUp -= OnPointerUp;
        }

        // ------------------------
        // POINTER DOWN
        // ------------------------
        private void OnPointerDown(Vector2 screenPos)
        {
            var ray = Camera.ScreenPointToRay(screenPos);

            // UI FIRST
            if (TryPickUI(ray, out var uiReceiver))
            {
                _pressedReceiver = uiReceiver;
                _pressedIsUI = true;
                return;
            }

            // BOARD SECOND
            if (TryPickBoard(ray, out var boardReceiver))
            {
                _pressedReceiver = boardReceiver;
                _pressedIsUI = false;
                return;
            }

            _pressedReceiver = null;
        }

        // ------------------------
        // POINTER UP → CLICK
        // ------------------------
        private void OnPointerUp(Vector2 screenPos)
        {
            if (_pressedReceiver == null)
                return;

            var ray = Camera.ScreenPointToRay(screenPos);

            if (TryGetHitPoint(ray,
                    _pressedIsUI ? UILayerMask : BoardLayerMask,
                    out var worldPos))
            {
                _pressedReceiver.OnPointerClicked(worldPos);
            }

            _pressedReceiver = null;
        }

        // ------------------------
        // UI PICK (UIOrder-based)
        // ------------------------
        private bool TryPickUI(Ray ray, out IInputReceiverUI bestReceiver)
        {
            bestReceiver = null;

            var count = Physics.RaycastNonAlloc(ray, _hits, MaxDistance, UILayerMask, _triggers);
            if (count <= 0) return false;

            var bestLayer = int.MinValue;
            var bestOrder = int.MinValue;
            var bestDist = float.PositiveInfinity;

            for (var i = 0; i < count; i++)
            {
                var hit = _hits[i];
                if (hit.collider == null) continue;

                var receiver = hit.collider.GetComponentInParent<IInputReceiverUI>();
                if (receiver == null) continue;

                var layer = receiver.UILayer;
                var order = receiver.UIOrder;

                var better =
                    layer > bestLayer ||
                    (layer == bestLayer && order > bestOrder) ||
                    (layer == bestLayer && order == bestOrder && hit.distance < bestDist);

                if (!better) continue;

                bestLayer = layer;
                bestOrder = order;
                bestDist = hit.distance;
                bestReceiver = receiver;
            }

            return bestReceiver != null;
        }

        // ------------------------
        // BOARD PICK (single hit)
        // ------------------------
        private bool TryPickBoard(Ray ray, out IInputReceiver receiver)
        {
            receiver = null;

            if (!Physics.Raycast(
                ray, out var hit, MaxDistance, BoardLayerMask, _triggers))
                return false;

            receiver = hit.collider.GetComponentInParent<IInputReceiver>();
            return receiver != null;
        }

        // ------------------------
        // HIT POINT
        // ------------------------
        private bool TryGetHitPoint(Ray ray, LayerMask mask, out Vector3 point)
        {
            if (Physics.Raycast(ray, out var hit, MaxDistance, mask, _triggers))
            {
                point = hit.point;
                return true;
            }

            point = default;
            return false;
        }
    }
}