using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    public static GlobalUpdate instance;
    public void Start()
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; }

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        for (int i = 0; i < MonoCache.allUpdates.Count; i++) MonoCache.allUpdates[i].Tick();
    }
}
