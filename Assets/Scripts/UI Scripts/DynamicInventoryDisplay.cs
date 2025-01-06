using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

public class DynamicInventoryDisplay : InventoryDisplay
{
	[SerializeField] protected InventorySlot_UI slotPrefab;

	protected override void Start()
	{
		base.Start();
	}

	private void OnDestroy()
	{
		InventoryHolder.OnDynamicInventoryDisplayRequested -= RefreshDynamicInventory;

	}

	public void RefreshDynamicInventory(InventorySystem invToDisplay, int offset)
	{
		ClearSlots();
		inventorySystem = invToDisplay;
		if(inventorySystem != null) inventorySystem.OnInventorySlotChanged += UpdateSlot;
		AssignSlot(invToDisplay, offset);
	}

	public override void AssignSlot(InventorySystem invToDisplay,int offset)
	{
		slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

		if (invToDisplay == null) return;

		for (int i = offset; i < invToDisplay.InventorySize; i++)
		{
			var uiSlot = Instantiate(slotPrefab, transform);
			slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
			uiSlot.Init(invToDisplay.InventorySlots[i]);
			uiSlot.UpdateUISlot();
		}

	}

	private void ClearSlots()
	{
		foreach (var item in transform.Cast<Transform>())
		{
			Destroy(item.gameObject);
		}

		slotDictionary?.Clear();
	}


}
