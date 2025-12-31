using Grid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager GridManager;
    void Start()
    {
        GridManager.Initialize();
    }

    void Update()
    {
        
    }
}
