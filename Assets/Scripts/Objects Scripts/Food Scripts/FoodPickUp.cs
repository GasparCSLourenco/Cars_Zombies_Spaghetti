using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

[RequireComponent(typeof(Collider))]

public class FoodPickUp : MonoBehaviour
{
	public float PickUpRadius = 1.0f;

	public FoodData FoodData;

	public SphereCollider myCollider;

	private string id;

	[SerializeField] private FoodPickUpSaveData saveData;


	private void Awake()
	{
		SaveLoad.OnLoadGame += LoadGame;
		saveData = new FoodPickUpSaveData(FoodData, transform.position, transform.rotation);
		myCollider = GetComponent<SphereCollider>();
		myCollider.isTrigger = true;
		myCollider.radius = PickUpRadius;
		id = GetComponent<UniqueId>().ID;	

	}

	private void Start()
	{
		SaveGameManager.data.activeFoods.Add(id, saveData);
		
	}

	private void LoadGame(SaveData data)
	{
		if (data.collectedItems.Contains(id))
		{
			Destroy(this.gameObject);
		}
	}

	private void OnDestroy()
	{
		if (SaveGameManager.data.activeFoods.ContainsKey(id))
		{
			SaveGameManager.data.activeFoods.Remove(id);
			SaveLoad.OnLoadGame -= LoadGame;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
		

		if(!inventory) return; //Checks if the object that has collided has an inventory

		if (inventory.AddToInventory(FoodData, 1)) //if it's possible to add the food to the inventory do so and delete food object.
		{
			SaveGameManager.data.collectedItems.Add(id);
			Destroy(this.gameObject);
		}
		
	}
}

[System.Serializable]
public struct FoodPickUpSaveData
{
	public FoodData FoodData;
	public Vector3 Position;
	public Quaternion Rotation;

    public FoodPickUpSaveData(FoodData _foodData, Vector3 _position, Quaternion _rotation)
    {
        FoodData = _foodData;
		Position = _position;
		Rotation = _rotation;
    }
}
