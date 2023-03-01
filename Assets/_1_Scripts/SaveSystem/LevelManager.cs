
using UnityEngine.Tilemaps;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    //public static LevelManager instance; 
    public List<TileData> tiles = new List<TileData>();    
    //void Awake()
    //{
    //    if (instance == null) instance = this;
    //    else Destroy(this);
    //}
    public List<Tilemap> tilemaps;
    public void SaveLevel() 
    {
        foreach (var maps in tilemaps)
        {
            BoundsInt bounds = maps.cellBounds;
            LevelData leveldata = new LevelData();
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    TileBase temp = maps.GetTile(new Vector3Int(x, y, 0));
                    TileData temptile = tiles.Find(t => t.tileBase == temp);
                    if (temptile != null)
                    {
                        leveldata.tiles.Add(temptile.id);
                        leveldata.posX.Add(x);
                        leveldata.posY.Add(y);
                    }
                }
            }
            string json = JsonUtility.ToJson(leveldata, true);
            File.WriteAllText(Application.dataPath + $"/testlevel{maps.name}.json ", json);
            Debug.Log("Level saved");
        }

    }
    public void LoadLevel() 
    {
        foreach (var maps in tilemaps)
        {
            string json = File.ReadAllText(Application.dataPath + $"/testlevel{maps.name}.json ");
            LevelData leveldata = JsonUtility.FromJson<LevelData>(json);

            maps.ClearAllTiles();

            for (int i = 0; i < leveldata.tiles.Count; i++)
            {
                maps.SetTile(new Vector3Int(leveldata.posX[i], leveldata.posY[i], 0), tiles.Find(t => t.id == leveldata.tiles[i]).tileBase);
            }
        } 
    }
}
public class LevelData
{
    public List<string> tiles = new();
    public List<int> posX = new();
    public List<int> posY = new();
}

//IEnumerable<LevelTile> GetTilesFromMap(Tilemap map)
//{
//    foreach (var pos in map.cellBounds.allPositionsWithin)
//    {
//        if (map.HasTile(pos))
//        {
//            var levelTile = map.GetTile<LevelTile>(pos);
//            yield return new LevelTile()
//            {
//                position = pos,
//                tile = levelTile
//            };
//        }
//    }
//}