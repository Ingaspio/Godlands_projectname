using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoCache
{
    [SerializeField] private Canvas canvas;
    
    
    //Zamenit' potom
    [SerializeField] private UI_InventoryItem item;
    private void Awake() 
    {
        canvas = transform.root.GetComponent<Canvas>();

        item = GetComponentInChildren<UI_InventoryItem>();
    }
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }
    public override void OnTick()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, Input.mousePosition,
            canvas.worldCamera, out position
        );
        transform.position = canvas.transform.TransformPoint(position);
    }
    public void Toggle(bool val)
    {
        //Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
