using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

public class StaticInventoryDisplay : InventoryDisplay
{

	[SerializeField] private InventoryHolder inventoryHolder;
	[SerializeField] private InventorySlot_UI[] slots;





	private void OnEnable()
	{
		PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;
	}

	private void OnDisable()
	{
		PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
	}

	private void RefreshStaticDisplay()
	{

		if (inventoryHolder != null)
		{
			inventorySystem = inventoryHolder.PrimaryInventorySystem;
			inventorySystem.OnInventorySlotChanged += UpdateSlot;
		}
		else Debug.LogWarning($"No Inventory Slot assigned to {this.gameObject}");

		AssignSlot(InventorySystem, 0);
	}

	protected override void Start()
	{
		base.Start();
		RefreshStaticDisplay();
	}

	public override void AssignSlot(InventorySystem invToDisplay, int offset)
	{
		slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

		for (int i = 0; i < inventoryHolder.Offset; i++)
        {
			slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
			slots[i].Init(InventorySystem.InventorySlots[i]);
        }
    }
}

	
