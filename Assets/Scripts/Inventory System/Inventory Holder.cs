using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;
	[SerializeField] protected int offset = 10;


	public int Offset => offset;
	public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested; //Inv system to display, Amount to offset display by

	protected virtual void Awake()
	{
		primaryInventorySystem = new InventorySystem(inventorySize);
		SaveLoad.OnLoadGame += LoadInventory;
	}

	protected abstract void LoadInventory(SaveData data);
}

[System.Serializable]
public struct InventorySaveData
{
	public InventorySystem InvSystem;
	public Vector3 Position;
	public Quaternion Rotation;

	public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
	{
		InvSystem = _invSystem;
		Position = _position;
		Rotation = _rotation;
	}

    public InventorySaveData(InventorySystem _invSystem)
    {
		InvSystem = _invSystem;
		Position = Vector3.zero;
		Rotation = Quaternion.identity;
    }
}
