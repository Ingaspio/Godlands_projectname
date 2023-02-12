using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "SO/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject   
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
