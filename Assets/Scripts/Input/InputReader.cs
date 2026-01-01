using System;
using UnityEngine;

namespace Input
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2> PointerDown; 
        public event Action<Vector2> PointerUp;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                PointerDown?.Invoke(UnityEngine.Input.mousePosition);

            if (UnityEngine.Input.GetMouseButtonUp(0))
                PointerUp?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}