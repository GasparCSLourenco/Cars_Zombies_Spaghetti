using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FoodPickUp : MonoBehaviour
{
	public float PickUpRadius = 1.0f;

	public FoodData FoodData;

	public SphereCollider myCollider;


	private void Awake()
	{
		myCollider = GetComponent<SphereCollider>();
		myCollider.isTrigger = true;
		myCollider.radius = PickUpRadius;

	}

	private void OnTriggerEnter(Collider other)
	{
		var inventory = other.transform.GetComponent<InventoryHolder>();

		if(!inventory) return;

		if (inventory.InventorySystem.AddToInventory(FoodData, 1))
		{
			Destroy(this.gameObject);
		}
	}
}
