using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/Food Data")]
public class FoodData : ScriptableObject
{

	public int Id;
	public string Name;
	[TextArea(4,4)]
	public string Description;
	public List<ProcessingObjectsData> Objects;
	public Sprite Icon;
	public int MaxStackSize;
}
