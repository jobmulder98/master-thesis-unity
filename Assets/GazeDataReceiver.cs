using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobii.XR;
using System.IO;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeDataReceiver : MonoBehaviour
{
    static public int userID = 0;
    static public string condition = "baseline";
    static public string dataDirectory = "Data";
    private string fileName = Directory.GetCurrentDirectory() + "\\" + dataDirectory + "\\datafile_" + condition + "_" + userID.ToString() + ".csv";
    private int frame;
    private string focusObject;
    private float timeStamp;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    float convergenceDistance;
    public countObjectsColliding countObjectsColliding;
    public Rigidbody RigidBodyHMD;
    public Rigidbody RigidBodyLeftController;
    public Rigidbody RigidBodyRightController;
    private XRGrabInteractable interactor;
    public bool isGrabbing;

    // Start is called before the first frame update
    void Awake()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
    }

    void Start()
    {
        CreateOutput();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 HMDPosition = RigidBodyHMD.position;
        Quaternion HMDRotation = RigidBodyHMD.rotation;
        Vector3 HMDVelocity = RigidBodyHMD.velocity;

        Vector3 LeftControllerPosition = RigidBodyLeftController.position;
        Quaternion LeftControllerRotation = RigidBodyLeftController.rotation;
        Vector3 LeftControllerVelocity = RigidBodyLeftController.velocity;

        Vector3 RightControllerPosition = RigidBodyRightController.position;
        Quaternion RightControllerRotation = RigidBodyRightController.rotation;
        Vector3 RightControllerVelocity = RigidBodyRightController.velocity;

        int objectsInCart = countObjectsColliding.objectsColliding;

        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        timeStamp = eyeTrackingData.Timestamp;

        // add try block to this component
        var grabbedItem = GetComponent<XRGrabInteractable>().name;
        
        // if (grabbedItem.isSelected)

        if (eyeTrackingData.GazeRay.IsValid)
        {
            rayOrigin = eyeTrackingData.GazeRay.Origin;
            rayDirection = eyeTrackingData.GazeRay.Direction;
        }
        else
        {
            rayOrigin = new Vector3(0, 0, 0);
            rayDirection = new Vector3(0, 0, 0);
        }
        
        if (eyeTrackingData.ConvergenceDistanceIsValid)
        {
            convergenceDistance = eyeTrackingData.ConvergenceDistance;
        }
        else
        {
            convergenceDistance = 0;
        }
                
        var eyeTrackingDataLocal = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);
        var isLeftEyeBlinking = eyeTrackingDataLocal.IsLeftEyeBlinking;
        var isRightEyeBlinking = eyeTrackingDataLocal.IsRightEyeBlinking;
        var eyesDirection = eyeTrackingDataLocal.GazeRay.Direction;

        if (TobiiXR.FocusedObjects.Count > 0)
        {
            var focusedGameObject = TobiiXR.FocusedObjects[0].GameObject;
            focusObject = focusedGameObject.name;
        }
        else
        {
            focusObject = "notAssigned";
        }

        frame++;
        WriteOutput(
            rayOrigin, 
            rayDirection, 
            convergenceDistance,
            isLeftEyeBlinking, 
            isRightEyeBlinking,
            eyesDirection,
            focusObject,
            objectsInCart, 
            HMDPosition, 
            HMDRotation, 
            HMDVelocity,
            LeftControllerPosition,
            LeftControllerRotation,
            LeftControllerVelocity,
            RightControllerPosition,
            RightControllerRotation,
            RightControllerVelocity
            );
    }

    void CreateOutput()
    {
        string variable =
        "timeStamp;" +
        "frame;" +
        "rayOrigin;" +
        "rayDirection;" +
        "convergenceDistance;" +
        "isLeftEyeBlinking;" +
        "isRightEyeBlinking;" + 
        "eyesDirection;" +
        "focusObject;" +
        "objectsInCart;" +
        "HMDposition;" +
        "HMDrotation;" +
        "HMDvelocity;" +
        "LeftControllerPosition;" +
        "LeftControllerRotation;" +
        "LeftControllerVelocity;" +
        "RightControllerPosition;" +
        "RightControllerRotation;" +
        "RightControllerVelocity;" +
        Environment.NewLine;

        File.AppendAllText(fileName, variable);
    }

    void WriteOutput(
        Vector3 rayOrigin,
        Vector3 rayDirection,
        float convergenceDistance,
        bool isLeftEyeBlinking,
        bool isRightEyeBlinking,
        Vector3 eyesDirection,
        string focusObject,
        int objectsInCart,
        Vector3 HMDposition,
        Quaternion HMDrotation,
        Vector3 HMDvelocity,
        Vector3 LeftControllerPosition,
        Quaternion LeftControllerRotation,
        Vector3 LeftControllerVelocity,
        Vector3 RightControllerPosition,
        Quaternion RightControllerRotation,
        Vector3 RightControllerVelocity
        )
    {
        string value =
            timeStamp.ToString() + ";" +
            frame.ToString() + ";" +
            rayOrigin.ToString() + ";" +
            rayDirection.ToString() + ";" +
            convergenceDistance.ToString() + ";" +
            isLeftEyeBlinking.ToString() + ";" +
            isRightEyeBlinking.ToString() + ";" +
            eyesDirection.ToString() + ";" +
            focusObject + ";" +                         // No need for ToString()
            objectsInCart.ToString() + ";" +
            HMDposition.ToString() + ";" +
            HMDrotation.ToString() + ";" +
            HMDvelocity.ToString() + ";" +
            LeftControllerPosition.ToString() + ";" +
            LeftControllerRotation.ToString() + ";" +
            LeftControllerVelocity.ToString() + ";" +
            RightControllerPosition.ToString() + ";" +
            RightControllerRotation.ToString() + ";" +
            RightControllerVelocity.ToString() + ";" +
            Environment.NewLine;

        File.AppendAllText(fileName, value);
    }
}
