using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGeneration : MonoBehaviour
{
    public string gameSeed = "Default";
    public int currentSeed = 0;
    public bool randomisedSeed = false;


    private void Awake()
    {
        if (randomisedSeed == false) { currentSeed = gameSeed.GetHashCode(); }
        if (randomisedSeed == true) { currentSeed = Random.Range(0, 9999999); }
    }
}
