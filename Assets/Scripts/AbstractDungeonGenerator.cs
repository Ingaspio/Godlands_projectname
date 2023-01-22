using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
   
    

    public void GenerateDungeon() 
    { 
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }
    public void ClearDungeon() { tilemapVisualizer.Clear(); }

    protected abstract void RunProceduralGeneration();
}
