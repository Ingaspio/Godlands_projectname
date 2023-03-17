using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public string dungeonSceneName;
    public void EnterDungeonScene() 
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (player.transform.position == transform.position && Input.GetKeyDown("e"))
        {
            SaveLoadUtility saveLoadUtility = FindObjectOfType<SaveLoadUtility>();
            saveLoadUtility.SaveGame(SceneManager.GetActiveScene().name);
            levelManager.SaveLevel();
            SceneManager.LoadScene(dungeonSceneName);
            //saveLoadUtility.LoadGame(dungeonSceneName);
            //levelManager.LoadLevel(dungeonSceneName);
        }
  
    }    
}
