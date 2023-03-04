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
        if (player.transform.position == transform.position && Input.GetKeyDown("e"))
           SceneManager.LoadScene(dungeonSceneName);
        
    }    
}
