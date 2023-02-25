using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;
using DG.Tweening;
using Input = UnityEngine.Input;

public class PathfindMovement : MonoBehaviour
{
    [SerializeField]
    Tilemap floor;
    public Transform startPos;
    public Transform endPos;
    [SerializeField]
    LineRenderer linePath;
    List<Vector3> wayPoints;

    private void Start()
    {
        wayPoints = new List<Vector3>();
    }
    public void PathfindMove() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            endPos.position = mousePos;
            wayPoints = AStar.FindPathClosest(floor, startPos.position, endPos.position);
            if (wayPoints != null)
            {
                for (int i = 0; i < wayPoints.Count; i++)
                {
                    //Debug.Log("Point: " + i + ", position: " + wayPoints[i]);
                    linePath.positionCount = wayPoints.Count;
                    linePath.SetPositions(wayPoints.ToArray());
                    startPos.DOPath(wayPoints.ToArray(),1f);
                }
            }
        }
    }
}
