using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    EntranceRoutine entranceRoutine;    
    [SerializeField]
    private float testAddvisionAmount;
    [SerializeField]
    private FogOfWarScript fogOfWarScript;
    
    
    
    private void Start()
    {
        Entrance entrances = FindObjectOfType<Entrance>();
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        fogOfWarScript.PaintFoW();
        fogOfWarScript.PaintFoWExplored();
        player.PlayerDontDestroy();
        StartCoroutine(entranceRoutine.SceneChangeRoutine());
        entrances.EnterDungeonScene();
    }

    void Update()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        PathfindMovement movement = FindObjectOfType<PathfindMovement>();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            TileBase clickedTile = tilemap.GetTile(gridPosition);
        }
        fogOfWarScript.PaintFoWExplored();

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        fogOfWarScript.AddVision(playerPosition);
        movement.PathfindMove();
    }
}
