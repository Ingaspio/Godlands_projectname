using UnityEngine;

public class DDoLScript : MonoBehaviour
{
    public static DDoLScript instance;
    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else 
            instance = this;

        DontDestroyOnLoad(gameObject);
    } 
}
