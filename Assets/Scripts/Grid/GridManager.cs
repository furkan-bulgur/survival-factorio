using Grid.GridView;
using UnityEngine;

namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridBackground GridBackground;
        [SerializeField] private GridLines GridLines;
        [SerializeField] private GridProperties GridProperties;
        
        public void Initialize()
        {
            GridBackground.Initialize(GridProperties);
            GridLines.Initialize(GridProperties);
        }
    }
}