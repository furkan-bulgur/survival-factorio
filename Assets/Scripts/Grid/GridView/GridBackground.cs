using UnityEngine;

namespace Grid.GridView
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GridBackground : MonoBehaviour
    {
        private GridProperties _gridProperties;
        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void Initialize(GridProperties properties)
        {
            _gridProperties = properties;
            CreateBackground();
        }

        public void CreateBackground()
        {
            var mesh = new Mesh { name = "GridBackgroundMesh" };

            var w = _gridProperties.Width * _gridProperties.CellSize;
            var h = _gridProperties.Height * _gridProperties.CellSize;

            var origin = new Vector2(-w * 0.5f, -h * 0.5f);
            var zOffset = _gridProperties.BackgroundZOffset;

            Vector3[] verts =
            {
                new(origin.x,     origin.y,     zOffset),
                new(origin.x + w, origin.y,     zOffset),
                new(origin.x + w, origin.y + h, zOffset),
                new(origin.x,     origin.y + h, zOffset)
            };

            int[] tris =
            {
                0, 2, 1,
                0, 3, 2
            };

            mesh.vertices  = verts;
            mesh.triangles = tris;
            mesh.RecalculateBounds();

            _meshFilter.sharedMesh = mesh;
        }
    }
}
