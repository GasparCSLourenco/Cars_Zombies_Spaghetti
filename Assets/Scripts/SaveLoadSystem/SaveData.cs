using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveData
{
	public SerializableDictionary<string, InventorySaveData> chestDictionary;
	public SerializableDictionary<string, FoodPickUpSaveData> activeFoods;
    public InventorySaveData playerInventory;
    

	public List<string> collectedItems; //List of Id's of items
    public SaveData()
    {
        collectedItems = new List<string>();    
        chestDictionary = new SerializableDictionary<string, InventorySaveData> ();
        activeFoods = new SerializableDictionary<string, FoodPickUpSaveData> ();
        playerInventory = new InventorySaveData ();
    }
}
