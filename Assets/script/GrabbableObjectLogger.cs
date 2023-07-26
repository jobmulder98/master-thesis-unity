using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObjectLogger : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public bool grabbingObject = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabbingObject = true;
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabbingObject = false;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Object " + gameObject.name);

    }
}
