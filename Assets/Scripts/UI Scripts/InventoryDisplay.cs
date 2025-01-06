using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

public abstract class InventoryDisplay : MonoBehaviour
{
	[SerializeField]
	MouseItemData mouseInventoryItem;
	protected InventorySystem inventorySystem;
	protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
	public InventorySystem InventorySystem => inventorySystem;
	public Dictionary<InventorySlot_UI,InventorySlot> SlotDictionary => slotDictionary;

	protected virtual void Start()
	{

	}

	public abstract void AssignSlot(InventorySystem invToDisplay, int offset);

	protected virtual void UpdateSlot(InventorySlot updatedSlot)
	{
		foreach (var slot in slotDictionary)
		{
			if(slot.Value == updatedSlot)
			{
				slot.Key.UpdateUISlot(updatedSlot);
			}
		}
	}

	public void SlotClicked(InventorySlot_UI clickedUISlot)
	{

		bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

		//if clicked slot has an item and mouse doesn't, pick up the food
		if(clickedUISlot.AssignedInventorySlot.FoodData != null && mouseInventoryItem.AssignedInventorySlot.FoodData == null) 
		{
			//if the shift is pressed split the stacks
			if(isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot)) //Split Stack
			{
				mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
				clickedUISlot.UpdateUISlot();
				return;
			}
			else //if it's not clicked 
			{
				mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

				clickedUISlot.ClearSlot();
				return;
			}

			
		}

		//Passing the slot in the mouse inventory slot to the clicked UI slot
		if(clickedUISlot.AssignedInventorySlot.FoodData == null && mouseInventoryItem.AssignedInventorySlot.FoodData != null)
		{
			clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
			clickedUISlot.UpdateUISlot();

			mouseInventoryItem.ClearSlot();
			return;
		}

		//If the clicked slot and the mouse slot has an Item...
		if (clickedUISlot.AssignedInventorySlot.FoodData != null && mouseInventoryItem.AssignedInventorySlot.FoodData != null)
		{

			bool isSameItem = clickedUISlot.AssignedInventorySlot.FoodData == mouseInventoryItem.AssignedInventorySlot.FoodData;
			//If the items are the same and the clicked slot has room for the amount in the mouse item add the stacks
			if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
			{
				clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
				clickedUISlot.UpdateUISlot();
				mouseInventoryItem.ClearSlot();
				return;
			}
			else if(isSameItem && !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int roomLeftInStack))
			{
				if (roomLeftInStack < 1) SwapSlots(clickedUISlot); //If stack is full swap the items
				else //If stock is not full, take what is needed to make it full
				{
					int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - roomLeftInStack;

					clickedUISlot.AssignedInventorySlot.AddToStack(roomLeftInStack);
					clickedUISlot.UpdateUISlot();

					var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.FoodData, remainingOnMouse);
					mouseInventoryItem.ClearSlot();
					mouseInventoryItem.UpdateMouseSlot(newItem);
					return;
				}
					
			}
			//if the items are not the same swap them.
			else if (!isSameItem)
			{
				SwapSlots(clickedUISlot);
				return;
			}
		}
	}

	//Clone the mouse inventory slot, clearing it and then passing it to the UI slot clicked and updating the UI.
	public void SwapSlots(InventorySlot_UI clickedUISlot)
	{
		var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.FoodData, mouseInventoryItem.AssignedInventorySlot.StackSize);
		mouseInventoryItem.ClearSlot();
		mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

		clickedUISlot.ClearSlot();
		clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
		clickedUISlot.UpdateUISlot();
	}
	



}
