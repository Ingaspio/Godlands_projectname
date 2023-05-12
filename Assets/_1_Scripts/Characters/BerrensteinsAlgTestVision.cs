using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BerrensteinsAlgTestVision : MonoCache
{   
    public int vision = Mathf.Max(1, Mathf.Min(10, 3));
    public Vector3 tileAnchor;
    
    
    float timer = 0f;
    private float updateTimer = 0.6f;

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

    public void AddVision()
    {
        TilemapVisualizer tilemapVisualizer = FindObjectOfType<TilemapVisualizer>();
        Tilemap fow = GameObject.Find("FogOfWar").GetComponent<Tilemap>();
        Tilemap fowExplored = GameObject.Find("FogOfWarExplored").GetComponent<Tilemap>();

        tilemapVisualizer.ClearTiles(GetVisionPlayerCircle(), fow);
        tilemapVisualizer.ClearTiles(GetVisionPlayerCircle(), fowExplored);

    }
    private void Awake()
    {
        FogOfWarScript fogOfWarScript = FindObjectOfType<FogOfWarScript>();
        fogOfWarScript.PaintFoW();
    }
    public override void OnTick() 
    {
        FogOfWarScript fogOfWarScript = FindObjectOfType<FogOfWarScript>();
        timer += Time.deltaTime;
        if(timer >= updateTimer) 
        {
            fogOfWarScript.PaintFoWExplored();
            timer = 0f;
        }
        AddVision();

    }

}