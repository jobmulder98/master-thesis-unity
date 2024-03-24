using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerGrabLogger : MonoBehaviour
{
    private XRBaseInteractor interactor; // Reference to the XRBaseInteractor component
    public bool isGrabbing = false; // Flag to indicate if an object is currently being grabbed
    public string grabbedObjectName = "noObjectGrabbed"; // Name of the grabbed object

    private void Awake()
    {
        // Get reference to the XRBaseInteractor component
        interactor = GetComponent<XRBaseInteractor>();
    }

    private void Update()
    {
        // Check if the interactor has a selection (an object is being interacted with)
        if (interactor.hasSelection)
        {
            // Get the list of interactables currently selected by the interactor
            List<IXRSelectInteractable> interactables = interactor.interactablesSelected;

            // Iterate through each selected interactable
            foreach (IXRSelectInteractable interactable in interactables)
            {
                // Check if the interactable is a grabbable object
                if (interactable is XRGrabInteractable grabInteractable)
                {
                    // Update grabbed object name and set grabbing flag to true
                    grabbedObjectName = grabInteractable.gameObject.name;
                    isGrabbing = true;

                    // Assuming only one object can be grabbed at a time, exit loop
                    return;
                }
            }

            // Reset grabbed object name and grabbing flag if no grabbable object found
            grabbedObjectName = "noObjectGrabbed";
            isGrabbing = false;
        }
        else
        {
            // Reset grabbed object name and grabbing flag if no object is being interacted with
            grabbedObjectName = "noObjectGrabbed";
            isGrabbing = false;
        }
    }
}
