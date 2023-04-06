using CodeMonkey.Utils;
using UnityEngine;

public class UI_SeedInput : MonoBehaviour
{
    [SerializeField]
    private UI_InputWindow inputWindow;
    void Start()
    {
        transform.Find("New Game").GetComponent<Button_UI>().ClickFunc = () => inputWindow.Show("Test title", "empty");

    }
}
