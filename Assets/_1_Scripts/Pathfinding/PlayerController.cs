using UnityEngine.Tilemaps;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerController : MonoCache
{
    Transform startPos;
    Tilemap floor;
    Tilemap fow;
    public GridManager gridManager;


    void Start()
    {
        startPos = PlayerCharacter.instance.transform;
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        fow = GameObject.Find("FogOfWar").GetComponent<Tilemap>();
        gridManager = FindObjectOfType<GridManager>();
    }
    Vector2Int PlayerPositionReturner()
    {
        Vector2Int playerPos2Int = new Vector2Int(Mathf.FloorToInt(startPos.transform.position.x), Mathf.FloorToInt(startPos.transform.position.y));
        return playerPos2Int;
    }

    Vector2Int MousePositionReturner() 
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3Int gridPosition = floor.WorldToCell(mousePos);
        Vector2Int mousePos2Int = new Vector2Int(gridPosition.x, gridPosition.y);
        return mousePos2Int;
    }

    public override void OnTick()
    {
        

        if (Input.GetMouseButtonDown(0)) 
        {
            List<Vector3> list = gridManager.CreatePathV3(gridManager.CreatePath(PlayerPositionReturner(), MousePositionReturner()));
            startPos.DOPath(list.ToArray(), 1f);
        }
    }
}