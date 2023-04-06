using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeScript : MonoBehaviour
{
    public string sceneName;
    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }
}
