using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string regionName;
    public void ExitDungeonScene()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (player.transform.position == transform.position && Input.GetKeyDown("e"))
        {
            SaveLoadUtility saveLoadUtility = FindObjectOfType<SaveLoadUtility>();
            saveLoadUtility.SaveGame(SceneManager.GetActiveScene().name);
            levelManager.SaveLevel();
            SceneManager.LoadScene(regionName);
            levelManager.LoadLevel(regionName);
        }
            
    }
}
