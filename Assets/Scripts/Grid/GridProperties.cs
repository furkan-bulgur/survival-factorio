using UnityEngine;
using UnityEngine.Serialization;

namespace Grid
{
    [CreateAssetMenu(fileName = "GridProperties", menuName = "Scriptable Objects/GridProperties")]
    public class GridProperties : ScriptableObject
    {
        public int Width = 10;
        public int Height = 10;
        public float CellSize = 1f;
        public float LineThickness = 0.05f;
        public float BackgroundZOffset = 0.1f; // Behind grid lines
        public float LinesZOffset = 0.05f; // Behind grid lines
    }
}
