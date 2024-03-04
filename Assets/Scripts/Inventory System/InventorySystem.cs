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

	public bool AddToInventory(FoodData foodData, int amountToAdd) 
	{
		inventorySlots[0] = new InventorySlot(foodData,amountToAdd);
		return true;
	}


}
