using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceRoutine : MonoBehaviour
{
    public IEnumerator SceneChangeRoutine() 
    {
        Entrance entrance = FindObjectOfType<Entrance>();
        while (true)
        {
            entrance.EnterDungeonScene();
            yield return null;
        }
    }
}
