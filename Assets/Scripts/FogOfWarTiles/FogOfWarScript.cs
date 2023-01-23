using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class FogOfWarScript : MonoBehaviour
{
    //public GameObject playersTP;
    public Tilemap floorTilemap;
    public float anchor = 0.5f;
    public int offset = 35;
    public FogOfWarVisualizer fogOfWarVisualizer;
    [SerializeField]
    protected Vector2Int fowSizeInt = new Vector2Int(50, 50);

    public void PaintFoW()
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWTiles(fowTiles);
    }
    public void PaintFoWExplored() 
    {
        HashSet<Vector2Int> fowTiles = FoWTilesCreation(fowSizeInt);
        fogOfWarVisualizer.PaintFoWExploredTiles(fowTiles);
    }
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



    
    
    
    
    
    
    
    
    
    
    
    
    
    //public static HashSet<Vector2Int> PlayerVisionGenerator(Vector2Int playerPosition, int visionLength)
    //{
    //    HashSet<Vector2Int> fieldOfView = new HashSet<Vector2Int>();

    //    fieldOfView.Add(playerPosition);
    //    var previousPosition = playerPosition;

    //    for (int i = 0; i < visionLength; i++)
    //    {
    //        var newPosition = previousPosition + Direction2D.eightDirectionsList[i];
    //        fieldOfView.Add(newPosition);
    //        previousPosition = newPosition;
    //    }
    //    return fieldOfView;
    //}


    //public void PlayerVisionPainter() 
    //{
    //    int xRound = Mathf.RoundToInt(playersTP.transform.position.x);
    //    int yRound = Mathf.RoundToInt(playersTP.transform.position.y);
    //    Vector2Int playerPosition = new Vector2Int(xRound, yRound);
    //    HashSet<Vector2Int> playersVision = PlayerVisionGenerator(playerPosition, 4);
        
    //}
        

    

