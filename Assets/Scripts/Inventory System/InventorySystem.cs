using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using JetBrains.Annotations;

[System.Serializable]
public class InventorySystem
{

	[SerializeField]
	private List<InventorySlot> inventorySlots;

	public List<InventorySlot> InventorySlots => inventorySlots;

	public int InventorySize => inventorySlots.Count;

	public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

		for (int i = 0; i < size; i++) 
		{
			inventorySlots.Add(new InventorySlot());
		}
    }

	public bool AddToInventory(FoodData itemToAdd, int amountToAdd) 
	{
		if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot)) //Check if item exists in Inventory
		{
            foreach (var slot in inventorySlot)
            {
				if (slot.AmountLeftInStack(amountToAdd))
				{
					slot.AddToStack(amountToAdd);
					OnInventorySlotChanged?.Invoke(slot);
					return true;
				}
            }
		}

		if (HasFreeSlot(out InventorySlot freeSlot)) //Get first available slot
		{
			freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
			OnInventorySlotChanged?.Invoke(freeSlot);
			return true;
		}
		return false;
	}

	public bool ContainsItem(FoodData itemToAdd, out List<InventorySlot> invSlot) 
	{
		invSlot = InventorySlots.Where(slot => slot.FoodObj == itemToAdd).ToList();

		return (invSlot != null); 
	}

	public bool HasFreeSlot(out InventorySlot freeSlot)
	{
		freeSlot = InventorySlots.FirstOrDefault(slot => slot.FoodObj == null);
		return freeSlot != null;
	}


}
