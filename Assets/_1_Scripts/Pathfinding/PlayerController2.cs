using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController2 : MonoCache
{
    public PathfindingCodeMonkey pathfinding;
    Tilemap floor;

    int2 MousePositionReturner()
    {
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3Int gridPosition = floor.WorldToCell(mousePos);
        int2 mousePos2Int = new int2(gridPosition.x, gridPosition.y);
        return mousePos2Int;
    }

    public override void OnTick()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            int2 playerPosInt2 = new int2((int)PlayerCharacter.instance.transform.position.x, (int)PlayerCharacter.instance.transform.position.y);
            pathfinding.FindPath(playerPosInt2, MousePositionReturner());
        }
        
    }
}
