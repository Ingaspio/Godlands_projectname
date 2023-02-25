using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceOrExit : MonoBehaviour
{
    public string dungeonSceneName;
    public string regionSceneName;
    [SerializeField]
    GameObject player; 
    public void EnterDungeonScene() 
    {
        if (player.transform.position == transform.position && Input.GetKeyDown("e"))
            SceneManager.LoadScene(dungeonSceneName);
    }
    public void ExitDungeonScene() 
    {
        SceneManager.LoadScene(regionSceneName);
    }
    
}
