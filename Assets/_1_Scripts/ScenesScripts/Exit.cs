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
            if (player.transform.position == transform.position && Input.GetKeyDown("e"))
                SceneManager.LoadScene(regionName);
            
    }
}
