using System.Collections.Generic;
using UnityEngine;

namespace Grid.GridView
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))] 
    public class GridLines : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private GridProperties _gridProperties;

        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void Initialize(GridProperties properties)
        {
            _gridProperties = properties;
            CreateGridLines();
        }

        private void CreateGridLines()
        {
            var mesh = new Mesh
            {
                name = "GridLinesMesh"
            };

            var width = _gridProperties.Width;
            var height = _gridProperties.Height;
            var cellSize = _gridProperties.CellSize;
            var lineThickness = _gridProperties.LineThickness;

            var origin = new Vector2(-width * cellSize * 0.5f, -height * cellSize * 0.5f);

            List<Vector3> vertices = new();
            List<int> triangles = new();

            void AddRect(float x, float y, float w, float h)
            {
                int start = vertices.Count;

                vertices.Add(new Vector3(x,     y,     0));
                vertices.Add(new Vector3(x + w, y,     0));
                vertices.Add(new Vector3(x + w, y + h, 0));
                vertices.Add(new Vector3(x,     y + h, 0));

                triangles.Add(start + 0);
                triangles.Add(start + 2);
                triangles.Add(start + 1);
                triangles.Add(start + 0);
                triangles.Add(start + 3);
                triangles.Add(start + 2);
            }

            float totalWidth  = width  * cellSize;
            float totalHeight = height * cellSize;

            // Vertical grid lines
            for (int x = 0; x <= width; x++)
            {
                float posX = origin.x + x * cellSize - lineThickness * 0.5f;
                AddRect(posX, origin.y, lineThickness, totalHeight);
            }

            // Horizontal grid lines
            for (int y = 0; y <= height; y++)
            {
                float posY = origin.y + y * cellSize - lineThickness * 0.5f;
                AddRect(origin.x, posY, totalWidth, lineThickness);
            }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();

            _meshFilter.sharedMesh = mesh;
        }
        //
        // // --- Utility (future use) ---
        // public Vector2Int WorldToCell(Vector2 worldPos)
        // {
        //     Vector2 origin = centered
        //         ? new Vector2(-width * cellSize * 0.5f, -height * cellSize * 0.5f)
        //         : Vector2.zero;
        //
        //     int x = Mathf.FloorToInt((worldPos.x - origin.x) / cellSize);
        //     int y = Mathf.FloorToInt((worldPos.y - origin.y) / cellSize);
        //
        //     return new Vector2Int(x, y);
        // }
        //
        // public Vector2 CellToWorld(Vector2Int cell)
        // {
        //     Vector2 origin = centered
        //         ? new Vector2(-width * cellSize * 0.5f, -height * cellSize * 0.5f)
        //         : Vector2.zero;
        //
        //     return origin + (Vector2)cell * cellSize + Vector2.one * cellSize * 0.5f;
        // }
    }
}
