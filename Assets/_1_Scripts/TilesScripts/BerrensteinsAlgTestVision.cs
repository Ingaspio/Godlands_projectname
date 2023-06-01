using System.Collections.Generic;
using UnityEngine;



public class BerrensteinsAlgTestVision
{
    private int vision;
    public Vector3 tileAnchor;

    public HashSet<Vector2Int> GetVisionPlayerCircle() 
    {
        BresenhamAlgorythm bresenhamAlgorythm = new();
        Vector3 playersPosition = PlayerCharacter.instance.transform.position;
        HashSet<Vector2Int> circleVision = new HashSet<Vector2Int>();
        for (int i = 0 ; i < GetEndPoints().Count; i++)
        {
            Vector2Int playerPosVector2Int = new Vector2Int((int)playersPosition.x, (int)playersPosition.y);
            HashSet<Vector2Int> visionLine = bresenhamAlgorythm.BresenhamsLineAlgorythm(playerPosVector2Int, GetEndPoints()[i]);
            circleVision.Add(playerPosVector2Int);
            circleVision.UnionWith(visionLine);
        }
        return circleVision;
    }

    public List<Vector2Int> GetEndPoints() 
    {
        Vector3 playersPosition = PlayerCharacter.instance.transform.position;
        vision = PlayerCharacter.instance.player.visionRadius;
        List<Vector2Int> endPoints = new();

        for (int x = -vision; x <= vision; x++)
        {
            for (int y = -vision; y <= vision; y++)
            {
                if (x*x + y*y <= vision * vision)
                {
                    endPoints.Add(new Vector2Int(x + (int)Mathf.Floor(playersPosition.x), y + (int)Mathf.Floor(playersPosition.y)));
                }
            }
        }
        return endPoints;
    }
}