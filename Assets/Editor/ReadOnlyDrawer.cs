using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var previousGuiState = GUI.enabled;

		GUI.enabled = false;

		EditorGUI.PropertyField(position, property, label);

		GUI.enabled=previousGuiState;
	}
}
