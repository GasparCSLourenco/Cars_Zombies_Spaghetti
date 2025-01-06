using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;
    [SerializeField] private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; } 

	private void Awake()
	{
        ClearSlot();

        button = GetComponent<Button>();
		button?.onClick.AddListener(OnUISlotClick);
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();

	}

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = string.Empty;
    }

    public void OnUISlotClick() 
    {
        ParentDisplay?.SlotClicked(this);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if (slot.FoodData != null)
        {
            itemSprite.sprite = slot.FoodData.Icon;
            itemSprite.color = Color.white;
			itemCount.text = (slot.StackSize > 1) ? slot.StackSize.ToString() : string.Empty;
		}
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if(assignedInventorySlot != null)
        {
            UpdateUISlot(assignedInventorySlot);
        }
    }
}
