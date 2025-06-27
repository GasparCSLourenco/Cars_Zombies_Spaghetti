using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
	public Transform interactionPoint;
	public LayerMask interactionLayer;
	public float interactionPointRadius = 1.0f;

	public bool IsInteracting;

	private void Update()
	{
		var colliders = Physics.OverlapSphere(interactionPoint.position,interactionPointRadius, interactionLayer);


		if(Keyboard.current.eKey.wasPressedThisFrame)
		{
			for (int i = 0; i < colliders.Length; i++)
			{
				var interactable = colliders[i].GetComponent<IInteractable>();
				

				if(interactable != null)
				{
					StartInteraction(interactable);
				}
			}
		}
	}

	void StartInteraction(IInteractable interactable)
	{
		interactable.Interact(this, out bool interactSuccessful);
		IsInteracting = true;
	}
	void EndInteraction()
	{
		IsInteracting = false;
	}
}
