using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
public class UI_InventoryPage : MonoBehaviour
{
    [SerializeField]
    private UI_InventoryItem itemPrefab;
    [SerializeField] 
    private RectTransform contentPanel;
    [SerializeField] 
    private UI_InventoryDescription itemDescription;
    [SerializeField] 
    private MouseFollower mouseFollower; 

    List<UI_InventoryItem> listOfUIItems= new List<UI_InventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    
    public event Action<int, int> OnSwapItems;

    private void Awake() 
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UI_InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightBtnMouseClick += HandleShowItemActions;
        }
    }
    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }
    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if(listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleItemSelection(UI_InventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        
        OnDescriptionRequested?.Invoke(index);
    }
    private void HandleBeginDrag(UI_InventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }
    public void CreateDraggedItem(Sprite image, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }
    private void HandleSwap(UI_InventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {  
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItemUI); 
    }   
    private void HandleEndDrag(UI_InventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }
    private void HandleShowItemActions(UI_InventoryItem inventoryItemUI)
    {

    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();        
    }
    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }
    public void DeselectAllItems()
    {
        foreach(UI_InventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
    public void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
    }
    public void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
}

