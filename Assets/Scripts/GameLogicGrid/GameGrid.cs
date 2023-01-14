using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameGrid : MonoBehaviour
{
    public Grid grid;
    
    public void LogWorldPosition(Grid grid) 
    {
        Vector3Int cellPosition = new Vector3Int();
        
        Mouse mouse = new Mouse();
        if (mouse.IsPressed()) 
        { 
            grid.CellToLocal(cellPosition); 
            Debug.Log(cellPosition.ToString());
        }
    }
    private void Update()
    {
        LogWorldPosition(grid);
    }
}
