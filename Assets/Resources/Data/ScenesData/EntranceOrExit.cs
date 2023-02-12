using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceOrExit : MonoBehaviour
{
    public string dungeonSceneName;
    public string regionSceneName;
    public void EnterDungeonScene() 
    {
        SceneManager.LoadScene(dungeonSceneName);
    }
    public void ExitDungeonScene() 
    {
        SceneManager.LoadScene(regionSceneName);
    }

}
