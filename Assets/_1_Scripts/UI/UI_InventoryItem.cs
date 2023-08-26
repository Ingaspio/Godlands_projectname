using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;


public class UI_InventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
    IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image borderImage;

    public event Action<UI_InventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightBtnMouseClick;

    private bool empty = true;

    public void Awake() 
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int itemQuantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = itemQuantity + "";
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }
    public void OnPointerClick(PointerEventData pointerData)
    {
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightBtnMouseClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
