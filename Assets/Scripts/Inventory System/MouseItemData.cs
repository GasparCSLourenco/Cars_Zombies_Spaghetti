using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite; //Item sprite for the item that will be attached to the mouse	
    public TextMeshProUGUI ItemCount; //Number of that item
	public InventorySlot AssignedInventorySlot; //Reference for the mouse inventory

	private void Awake()
	{
		ItemSprite.color = Color.clear;
		ItemCount.text = string.Empty;
	}

	public void UpdateMouseSlot(InventorySlot invSlot)
	{
		if (invSlot.FoodData != null)
		{
			AssignedInventorySlot.AssignItem(invSlot);
			ItemSprite.sprite = invSlot.FoodData.Icon;
			ItemSprite.color = Color.white;
			ItemCount.text = invSlot.StackSize.ToString();
		}
	}

	private void Update()
	{
		//TODO: Add controller support
		if (AssignedInventorySlot.FoodData!= null) 
		{
			transform.position = Mouse.current.position.ReadValue(); //If mouse inventory has an item, follow mouse Position

			if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject()) //Deletes the item when left clicking if the cursor is not over UI objects
			{
				ClearSlot();
			}
		}
	}

	public void ClearSlot() 
	{
		AssignedInventorySlot.ClearSlot();
		ItemCount.text = string.Empty;
		ItemSprite.sprite = null;
		ItemSprite.color = Color.clear;
	}

	public static bool IsPointerOverUIObject() //Checks if an object is hovering over any objects with raycast
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

}
