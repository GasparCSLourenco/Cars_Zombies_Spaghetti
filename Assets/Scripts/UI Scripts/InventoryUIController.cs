using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB


public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
	public DynamicInventoryDisplay playerBackpackPanel;


	private void Awake()
	{
		inventoryPanel.gameObject.SetActive(false);
		playerBackpackPanel.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
	}

	private void OnDisable()
	{
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;

	}

    // Update is called once per frame
    void Update()
    {

        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) inventoryPanel.gameObject.SetActive(false);

		if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) playerBackpackPanel.gameObject.SetActive(false);
	}

    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay,offset);
    }

	
}
