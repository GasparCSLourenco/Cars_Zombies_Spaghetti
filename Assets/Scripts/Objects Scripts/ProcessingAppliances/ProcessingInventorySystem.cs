using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingInventorySystem
{

    [SerializeField] private int ProcessingCapacity;
    [SerializeField] private int ProcessingStorage;

    [SerializeField] private List<FoodData> ProcessableFood;
    [SerializeField] private FoodData ProcessingFood;



    public ProcessingInventorySystem(int capacity)
    {
        ProcessingCapacity = capacity;
        ProcessingStorage = 0;

        ProcessableFood = new List<FoodData>();
        
    }


}
