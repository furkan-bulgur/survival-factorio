using Grid.GridView;
using Input;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour, IInputReceiver
    {
        [SerializeField] private GridBackground GridBackground;
        [SerializeField] private GridLines GridLines;
        [SerializeField] private GridProperties GridProperties;
        [SerializeField] private BoxCollider GridCollider;
        
        public void Initialize()
        {
            InitializeGrid();
            InitializeGridView();
        }

        private void InitializeGridView()
        {
            GridBackground.Initialize(GridProperties);
            GridLines.Initialize(GridProperties);
        }

        private void InitializeGrid()
        {
            var colliderWidth = GridProperties.Width * GridProperties.CellSize;
            var colliderHeight = GridProperties.Height *  GridProperties.CellSize;
            GridCollider.size = new Vector3(colliderWidth, colliderHeight, 1);
        }

        public void OnPointerClicked(Vector3 position)
        {
            // Debug log the grid cell that was clicked
            var localPos = transform.InverseTransformPoint(position);
            var cellX = Mathf.FloorToInt((localPos.x + (GridProperties.Width * GridProperties.CellSize * 0.5f)) / GridProperties.CellSize);
            var cellY = Mathf.FloorToInt((localPos.y + (GridProperties.Height * GridProperties.CellSize * 0.5f)) / GridProperties.CellSize);
            Debug.Log($"Grid cell clicked: ({cellX}, {cellY})");
        }
    }
}