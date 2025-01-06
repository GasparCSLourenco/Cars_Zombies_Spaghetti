using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private FoodData foodData; //Reference to the food data
    [SerializeField] private int stackSize; // Current stack size
    

    public FoodData FoodData => foodData;

    public int StackSize => stackSize;

    public InventorySlot(FoodData source, int amount) //Ctor to instantiate an inventory slot with food already in it.
    {
        foodData = source;
        stackSize = amount;
        
    }

    public InventorySlot() //Ctor to make an empty inventory slot
    {
        ClearSlot();
    }

    public void ClearSlot() 
    {
        foodData = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(FoodData data, int amount) //Updates slot directly
    {
        foodData = data;
        stackSize = amount;
        
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) //Checks for room left in stack and passes the amount remaining
    {
        
        amountRemaining = FoodData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);

    }

    public bool EnoughRoomLeftInStack(int amountToAdd) //Checks for the room left in stack without returning the amount remaining
    {
        if (foodData == null || foodData != null && stackSize + amountToAdd <= foodData.MaxStackSize) return true;
        else return false;
		
	}

    public void AddToStack(int amount) //Adds to the stack
    {
        stackSize += amount;
    }
    public void RemoveFromStack(int amount) //removes from the stack
    {
        stackSize -= amount;
    }

	public void AssignItem(InventorySlot invSlot) //Assigns an item to the slot
	{
		if(foodData == invSlot.foodData) AddToStack(invSlot.StackSize); //If the slot already contains the food that's trying to be added, just increase stack size
        else //overwrite the slot with the new food
        {
            foodData = invSlot.foodData;
            stackSize = 0;
            AddToStack(invSlot.StackSize);
        }
	}


    public bool SplitStack(out InventorySlot splitStack) //Checks if there is enough left to split and returns the remaining after splitting
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);

        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(foodData,halfStack); 
        return true;
    }
}
