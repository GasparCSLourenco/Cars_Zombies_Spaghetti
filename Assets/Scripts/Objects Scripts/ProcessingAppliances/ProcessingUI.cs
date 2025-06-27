using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProcessingUI : MonoBehaviour
{
    ProcessingAppliances Appliance;
    public GameObject gameObject;

    private Color[] stepVisualIndicator = {Color.yellow, Color.green};
    MeshRenderer meshRenderer;
    Color defaultColor;



    private void OnEnable()
	{
        Oven.OnProcessAdvanced += ProcessAdvancementChangeUI;
	}

	private void OnDisable()
	{
		Oven.OnProcessAdvanced -= ProcessAdvancementChangeUI;
	}

	// Start is called before the first frame update
	void Start()
    {
        Appliance = GetComponent<ProcessingAppliances>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProcessAdvancementChangeUI(int processStep)
    {
        if (processStep == 0) 
        {
            meshRenderer.material.color = defaultColor;
        }
        else
        {
            meshRenderer.material.color = stepVisualIndicator[processStep-1];
        }
    }

}
