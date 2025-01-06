using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code based on a tutorial by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB


[CreateAssetMenu(menuName = "Processing Objects")]
public class ProcessingObjectsData : ScriptableObject
{
    public int Id;
    public float ProcessingDuration;
    public int MaxStackSize;
}
