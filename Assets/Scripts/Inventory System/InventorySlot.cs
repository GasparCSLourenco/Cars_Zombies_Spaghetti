using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private FoodData foodObj;
    [SerializeField] private int stackSize;

    public FoodData FoodObj => foodObj;

    public int StackSize => stackSize;

    public InventorySlot(FoodData source, int amount)
    {
        foodObj = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot() 
    {
        foodObj = null;
        stackSize = -1;
    }

    public void UpdateInventorySlot(FoodData data, int amount)
    {
        foodObj = data;
        stackSize = amount;
    }

    public bool AmountLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = FoodObj.MaxStackSize - stackSize;
        return AmountLeftInStack(amountToAdd);

    }

    public bool AmountLeftInStack(int amountToAdd)
    {
        return (stackSize + amountToAdd<=FoodObj.MaxStackSize);

    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }
    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

}
