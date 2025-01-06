using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueId))]
public class ChestInventory : InventoryHolder, IInteractable
{

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

	protected override void Awake()
	{
		base.Awake();
		SaveLoad.OnLoadGame += LoadInventory;
	}

	private void Start()
	{
		var chestSaveData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation);

		SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueId>().ID, chestSaveData);
	}

	protected override void LoadInventory(SaveData data)
	{
		if (data.chestDictionary.TryGetValue(GetComponent<UniqueId>().ID, out InventorySaveData chestData))
		{
			this.primaryInventorySystem = chestData.InvSystem;
			this.transform.position = chestData.Position;
			this.transform.rotation = chestData.Rotation;
		}

	}

	public void Interact(Interactor interactor, out bool interactionSuccessful)
	{
		OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem,0);
		interactionSuccessful = true;
	}

	public void EndInteraction()
	{
		throw new System.NotImplementedException();
	}
}


