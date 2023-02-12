
using UnityEngine.Tilemaps;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Tilemap tilemap;
    public List<TileData> tiles = new();    
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void SaveLevel() 
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData leveldata = new();
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                TileData temptile = tiles.Find(t => t.tile == temp);
                if (temptile != null)
                {
                    leveldata.tiles.Add(temptile.id);
                    leveldata.positions.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        string json = JsonUtility.ToJson(leveldata, true);
        File.WriteAllText(Application.dataPath + "/testlevel.json",json);
        Debug.Log("Level saved");
    }
    public void LoadLevel() 
    {
        string json = File.ReadAllText(Application.dataPath + "/testlevel.json");
        LevelData leveldata = JsonUtility.FromJson<LevelData>(json);

        tilemap.ClearAllTiles();

        for (int i = 0; i < leveldata.positions.Count; i++)
        {
            tilemap.SetTile(leveldata.positions[i],tiles.Find(t=> t.name ==leveldata.tiles[i]).tile);
        }
    }
}
public class LevelData
{
    public List<string> tiles = new();
    public List<Vector3Int> positions = new();
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