using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string lastExitName;
    public void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerCharacter.instance.transform.position = transform.position;
        }     
    }
}
