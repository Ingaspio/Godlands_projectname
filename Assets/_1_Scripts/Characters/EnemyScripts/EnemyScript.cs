using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyScript : MonoCache
{
    public CharacterStatsSO enemyStatsSO;
    float timer = 0;
    float updateTimer = 2f;
    public TileData enemyTile;
    public TileData floorTile;
    Vector3Int previousPos;
    Tilemap TM;

    public TileChangeData EnemyTileChanger() 
    {
        Vector3Int enemyPos3Int = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
        TM = GameObject.Find("Floor").GetComponent<Tilemap>();
        TileChangeData tileChangeData = new TileChangeData(enemyPos3Int, enemyTile, Color.clear, Matrix4x4.identity);
        return tileChangeData;
    }
    public Vector3Int EnemyPositionReturner() 
    {
        Vector3Int enemyPos3Int = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
        return enemyPos3Int;
    }

    private void Start()
    {
        TM = GameObject.Find("Floor").GetComponent<Tilemap>();
        previousPos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
    }

    public override void OnTick()
    {
        if (previousPos != transform.position)
        {
            TM.SetTile(previousPos, floorTile);
            TM.SetTile(EnemyPositionReturner(), enemyTile);
            previousPos = EnemyPositionReturner(); 
        }
        
    }
}
