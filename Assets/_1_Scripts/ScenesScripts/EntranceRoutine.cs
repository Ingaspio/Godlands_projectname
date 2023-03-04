using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceRoutine : MonoBehaviour
{
    public IEnumerator SceneChangeRoutine() 
    {
        Entrance entrance = FindObjectOfType<Entrance>();
        Exit exit = FindObjectOfType<Exit>();
        while (true)
        {
            if (entrance != null)
                entrance.EnterDungeonScene();
            if (exit != null)
                exit.ExitDungeonScene();
            
            yield return null;
        }
    }
}
