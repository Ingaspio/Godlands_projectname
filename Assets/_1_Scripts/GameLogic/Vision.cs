using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vision : MonoBehaviour
{
    public TilemapVisualizer tilemapVisualizer;
    public Tilemap fow, fowExplored;
    BerrensteinsAlgTestVision berrensteinsAlg = new();
    private void Start()
    {
        //TilemapVisualizer tilemapVisualizer = FindObjectOfType<TilemapVisualizer>();
        //Tilemap fow = GameObject.Find("FogOfWar").GetComponent<Tilemap>();
        //Tilemap fowExplored = GameObject.Find("FogOfWarExplored").GetComponent<Tilemap>();
        //berrensteinsAlg = new BerrensteinsAlgTestVision();
        StartCoroutine(AddVisionDelay());
    }
    

    private void AddVision(TilemapVisualizer tilemapVisualizer, Tilemap fow, Tilemap fowExplored) 
    {
        tilemapVisualizer.ClearTiles(berrensteinsAlg.GetVisionPlayerCircle(), fow);
        tilemapVisualizer.ClearTiles(berrensteinsAlg.GetVisionPlayerCircle(), fowExplored);
    }
    IEnumerator AddVisionDelay() 
    {
        while (true)
        {
            if (transform.hasChanged) 
            {
                AddVision(tilemapVisualizer, fow, fowExplored);
                yield return new WaitForSeconds(2);
            }
        }
    }
}
