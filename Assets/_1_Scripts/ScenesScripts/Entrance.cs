using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoCache
{
    public string dungeonSceneName;
    public string exitName;

    public override void OnTick()
    {
        //LevelManager levelManager = FindObjectOfType<LevelManager>();
        
        //levelManager.SaveLevel();
        if (PlayerCharacter.instance.transform.position == transform.position && Input.GetKeyDown("e"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            SceneManager.LoadScene(dungeonSceneName);
        }
    }
}
