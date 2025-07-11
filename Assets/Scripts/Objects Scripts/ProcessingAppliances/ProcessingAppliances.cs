using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProcessingAppliances : MonoBehaviour, IInteractable
{
	public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	public void EndInteraction()
	{
		throw new System.NotImplementedException();
	}

	public void Interact(Interactor interactor, out bool interactionSuccessful)
	{
		throw new System.NotImplementedException();
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
