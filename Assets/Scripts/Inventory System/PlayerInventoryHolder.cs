using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB


public class PlayerInventoryHolder : InventoryHolder
{

	
	public static UnityAction OnPlayerInventoryChanged;

	private void Start()
	{
		SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
	}


	protected override void LoadInventory(SaveData saveData)
	{
		if (saveData.playerInventory.InvSystem != null)
		{
			this.primaryInventorySystem = saveData.playerInventory.InvSystem;
			OnPlayerInventoryChanged?.Invoke();
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (Keyboard.current.bKey.wasPressedThisFrame)
		{
			OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem,offset);
		}
    }

	public bool AddToInventory(FoodData food, int amount)
	{
		if(primaryInventorySystem.AddToInventory(food, amount)) 
		{
			OnPlayerInventoryChanged.Invoke();
			return true;
		}
		return false;
	}

	
}
