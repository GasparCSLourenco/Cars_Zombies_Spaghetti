using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

public class Oven : ProcessingAppliances, IInteractable
{

    [SerializeField] int ProcessingTime;

    public Dictionary<FoodData,FoodData> FoodConversionPair = new Dictionary<FoodData,FoodData>();
    public SaveGameManager SaveGameManager;
    public FoodDb foodDb;
    [SerializeField]private InventorySlot Storage = new InventorySlot();
    private int ProcessTime = 2;

    int ProcessStep = 0; //0 = process has not started, 1 = is processing, 2 = process has ended

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public static UnityAction<int> OnProcessAdvanced;

    FoodData foodToConvert;
    FoodData foodFromConversion;
    public bool isInteracting;


	// Start is called before the first frame update
	void Start()
    {
        foodDb = FindObjectOfType<FoodDb>();

        foodToConvert = foodDb.FoodDatabase[0];
        foodFromConversion = foodDb.FoodDatabase[2];

        FoodConversionPair.Add(foodToConvert, foodFromConversion);
        
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Interactor interactor, out bool interactionSuccesfull)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>().PrimaryInventorySystem;

        if (playerInv != null)
        {
            foreach (var food in FoodConversionPair.Keys)
            {
                var targetSlot = playerInv.InventorySlots.Where(slot => slot.FoodData == food).FirstOrDefault(); //Find the slot in the player's inv where the food can be processed by the appliance
                if (isInteracting)
                {
                    Debug.Log("Wait for process to be finished");
                }
                else
                {

					if (!isInteracting && Storage.FoodData != null)
					{
						playerInv.AddToInventory(Storage.FoodData, 1);
						Debug.Log(Storage.FoodData);
						Storage.ClearSlot();
                        ProcessStep = 0;
                        OnProcessAdvanced.Invoke(ProcessStep);
                        break;
					}

					if (targetSlot != null)
                    {
                        if (!isInteracting && Storage.FoodData == null)
                        {
                            isInteracting = true;
                            StartCoroutine(Process(targetSlot));
                            ProcessStep = 1;
                            OnProcessAdvanced?.Invoke(ProcessStep);

                        }

                        interactionSuccesfull = true;
                    }
                }
			}
        }
        interactionSuccesfull = false;
	}

    IEnumerator Process(InventorySlot targetSlot)
    {
		Storage.UpdateInventorySlot(foodFromConversion, 1);
		targetSlot.ClearSlot();
		PlayerInventoryHolder.OnPlayerInventoryChanged.Invoke();
		yield return new WaitForSeconds(ProcessTime);
		Debug.Log("Processing Complete");
		isInteracting = false;
        ProcessStep = 2;
        OnProcessAdvanced?.Invoke(ProcessStep);
	}

	public void EndInteraction()
	{
		throw new System.NotImplementedException();
	}

}
