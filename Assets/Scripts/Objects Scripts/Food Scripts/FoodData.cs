using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code based on a tutorial posted by Dan Pos. Youtube Video - https://www.youtube.com/watch?v=sPBhDcuBuIA&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&pp=iAQB

/// <summary>
/// Scriptable object that represents food that will be used by the players in the kitchen.
/// 
/// </summary>


[CreateAssetMenu(menuName = "Food/Food Data")]
[System.Serializable]
public class FoodData : ScriptableObject
{

	public int Id;
	public string Name;
	[TextArea(4,4)]
	public string Description;
	public Sprite Icon;
	public int MaxStackSize;
	
}
