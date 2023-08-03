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
        if (interactor.selectTarget)
        {
            XRBaseInteractable interactable = interactor.selectTarget;
            if (interactable != null && interactable is XRGrabInteractable grabInteractable)
            {
                grabbedObjectName = grabInteractable.gameObject.name;
                isGrabbing = true;
            }
        }
        else
        {
            // If not grabbing, log a message
            grabbedObjectName = "noObjectGrabbed";
            isGrabbing = false;
        }
    }
}
