using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_InputWindow : MonoBehaviour
{
    Button_UI okBtn;
    Button_UI cancelBtn;
    TextMeshProUGUI titleText;
    TMP_InputField inputField;
    private void Awake()
    {
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();
        titleText = transform.Find("titleText").GetComponent<TextMeshProUGUI>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();

        Hide();
    }
    public void Show(string titleString, string inputString) 
    { 
        gameObject.SetActive(true);

        titleText.text = titleString;
        inputField.text = inputString;
    }
    private void Hide() => gameObject.SetActive(false);
}
