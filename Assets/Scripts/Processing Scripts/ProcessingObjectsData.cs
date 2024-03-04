using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Processing Objects")]
public class ProcessingObjectsData : ScriptableObject
{
    public int Id;
    public float ProcessingDuration;
    public int MaxStackSize;
}
