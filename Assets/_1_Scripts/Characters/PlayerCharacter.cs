using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
   public static PlayerCharacter instance;
   public void PlayerDontDestroy() 
   {
        if (instance != null)
            Destroy(gameObject);
        else if (instance == null)
            Debug.Log("there is no player object");
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
   }
}
