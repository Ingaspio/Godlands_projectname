using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FogOfWarScript : MonoBehaviour
{
    public int offset = 35;
    public FogOfWarVisualizer fogOfWarVisualizer;
    //public FoWVision vision;
    [SerializeField]
    protected Vector2Int fowSizeInt = new Vector2Int(50, 50);

    public void PaintFoW()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWTilesInWorld(fowTiles);
    }
    public void PaintFoWExplored() 
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWExploredTilesInWorld(fowTiles);
    }
    //public void ClearFoW() 
    //{
    //    HashSet<Vector2> playersVision = vision.PlayerVisionInWorld();
    //    fogOfWarVisualizer.ClearFoWTilesInWorld(playersVision);
    //}
    public HashSet<Vector2Int> FoWTilesCreation(Vector2Int fowSize) 
    {

        HashSet<Vector2Int> fowTiles = new();
        for (int x = 0 - offset; x < fowSize.x; x++)
        {
            for (int y = 0 - offset; y < fowSize.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                fowTiles.Add(position); 
            }

        }
        return fowTiles; 
    }
}