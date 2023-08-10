using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerGrabLogger : MonoBehaviour
{
    private XRBaseInteractor interactor;
    public bool isGrabbing = false;
    public string grabbedObjectName = "noObjectGrabbed";

    private void Awake()
    {
        interactor = GetComponent<XRBaseInteractor>();
    }

    private void Update()
    {
        if (interactor.hasSelection)
        {
            List<IXRSelectInteractable> interactables = interactor.interactablesSelected;
            foreach (IXRSelectInteractable interactable in interactables)
            {
                if (interactable is XRGrabInteractable grabInteractable)
                {
                    grabbedObjectName = grabInteractable.gameObject.name;
                    isGrabbing = true;
                    return;
                }
            }
            grabbedObjectName = "noObjectGrabbed";
            isGrabbing = false;
        }
        else
        {
            grabbedObjectName = "noObjectGrabbed";
            isGrabbing = false;
        }
    }
}
