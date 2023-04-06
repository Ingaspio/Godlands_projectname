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
