using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SaveLoadUtility saveLoadUtility = FindObjectOfType<SaveLoadUtility>();
        saveLoadUtility.SaveGame(SceneManager.GetActiveScene().name);
        ObjectsPlacementOnTiles objectPlacement = FindObjectOfType<ObjectsPlacementOnTiles>();
        objectPlacement.PlaceObjects();
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        
        //fogOfWarScript.PaintFoW();
        fogOfWarScript.PaintFoWExplored();
        player.PlayerDontDestroy();
        StartCoroutine(entranceRoutine.SceneChangeRoutine());
        
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
