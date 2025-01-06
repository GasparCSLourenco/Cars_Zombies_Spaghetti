using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using JetBrains.Annotations;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

[System.Serializable]
public class InventorySystem
{

	[SerializeField]
	private List<InventorySlot> inventorySlots; //List of inventory slots that this system will hold. e.g.: each square in the hotbar

	public List<InventorySlot> InventorySlots => inventorySlots;

	public int InventorySize => inventorySlots.Count; //Total size of inventory. Quantity of inventory slots.

	public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size) //Instantiates empty inventory slots based on the inventory size
    {
        inventorySlots = new List<InventorySlot>(size);

		for (int i = 0; i < size; i++) 
		{
			inventorySlots.Add(new InventorySlot());
		}
    }

	public bool AddToInventory(FoodData itemToAdd, int amountToAdd) //Adds a food item to the inventory
	{
		if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot)) //Check if item exists in Inventory
		{
            foreach (var slot in inventorySlot)
            {
				if (slot.EnoughRoomLeftInStack(amountToAdd))
				{
					slot.AddToStack(amountToAdd);
					OnInventorySlotChanged?.Invoke(slot);
					return true;
				}
            }
		}

		if (HasFreeSlot(out InventorySlot freeSlot)) //Get first available slot
		{
			if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
			{
				freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
				OnInventorySlotChanged?.Invoke(freeSlot);
				return true;
			}
			
		}
		return false;
	}

	public bool ContainsItem(FoodData itemToAdd, out List<InventorySlot> invSlot)  //Returns a list of inventory slots that have a certain item in them
	{
		invSlot = InventorySlots.Where(slot => slot.FoodData == itemToAdd).ToList();

		return (invSlot != null); //If no slots contains that item, return null
	}

	public bool HasFreeSlot(out InventorySlot freeSlot) //Returns the first slot without any items in it.
	{
		freeSlot = InventorySlots.FirstOrDefault(slot => slot.FoodData == null);
		return freeSlot != null; //If all slots have items return null; 
	}


}
