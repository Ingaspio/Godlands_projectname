using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
   public static PlayerCharacter instance;
   public void Start() 
   {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; }

        DontDestroyOnLoad(gameObject);
   }
}
