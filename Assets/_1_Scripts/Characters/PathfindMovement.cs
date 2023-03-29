using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;
using DG.Tweening;
using Input = UnityEngine.Input;
using UnityEditor.UIElements;
using System.Linq;

public class PathfindMovement : MonoCache
{
    List<Vector3> wayPoints = new List<Vector3>();
    int tapTime = 0;
    public bool showDebug = true;
    
    public override void OnTick() 
    {
        Transform startPos = PlayerCharacter.instance.transform;
        Transform endPos = FindObjectOfType<Destination>().transform;
        LineRenderer linePath = FindObjectOfType<LineRenderer>();
        Tilemap walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        Tilemap floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        Tilemap fow = GameObject.Find("FogOfWar").GetComponent<Tilemap>();
        if (Input.GetMouseButtonDown(0)) 
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3Int gridPosition = floor.WorldToCell(mousePos);
            if (fow.HasTile(gridPosition)) { }
            else if (floor.HasTile(gridPosition)) 
            {
                tapTime++;
                if (tapTime == 1)
                {
                    endPos.position = mousePos;
                    wayPoints = AStar.FindPathClosest(walls, startPos.position, endPos.position);
                    if (wayPoints != null)
                    {
                        for (int i = 0; i < wayPoints.Count; i++)
                        {
                            linePath.positionCount = wayPoints.Count;
                            linePath.SetPositions(wayPoints.ToArray());
                        }
                    }
                    else { startPos.DOComplete(); }
                }
                if (tapTime == 2 && wayPoints != null && Vector3.Distance(wayPoints.LastOrDefault(), mousePos) < 0.25f)
                {
                    if (showDebug == true)
                        Debug.Log("Last Vector: " + wayPoints.LastOrDefault());
                    startPos.DOPath(wayPoints.ToArray(), 1f);
                }
                if (tapTime >= 2)
                    tapTime = 0;
            }
            
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            wayPoints.Clear();
            tapTime = 0;
        }
            
    }
}
