using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobii.XR;
using System.IO;
using UnityEngine.XR.Interaction.Toolkit;

public class DataCollector : MonoBehaviour
{
    public bool writeDataToFile = false;

    static public int userID = 0;
    static public string condition = "baseline";
    static public string dataDirectory = "Data";
    private string fileName = Directory.GetCurrentDirectory() + "\\" + dataDirectory + "\\datafile_" + condition + "_" + userID.ToString() + ".csv";
    private int frame;
    private float timeStamp;

    private string focusObjectName;
    private string focusObjectTag;

    Vector3 rayOrigin;
    Vector3 rayDirection;
    float convergenceDistance;

    public Rigidbody RigidBodyHMD;
    public Rigidbody RigidBodyLeftController;
    public Rigidbody RigidBodyRightController;

    private XRGrabInteractable interactor;
    public bool isGrabbing;

    public countObjectsColliding objectsCollidingScript;

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
            focusObjectName = focusedGameObject.name;
            focusObjectTag = focusedGameObject.tag;
        }
        else
        {
            focusObjectName = "notAssigned";
        }

        frame++;

        if (writeDataToFile)
        {
            WriteOutput(
                 rayOrigin,
                 rayDirection,
                 convergenceDistance,
                 isLeftEyeBlinking,
                 isRightEyeBlinking,
                 eyesDirection,
                 focusObjectName,
                 focusObjectTag,
                 HMDPosition,
                 HMDRotation,
                 HMDVelocity,
                 LeftControllerPosition,
                 LeftControllerRotation,
                 LeftControllerVelocity,
                 RightControllerPosition,
                 RightControllerRotation,
                 RightControllerVelocity,
                 objectsCollidingScript.numberOfItemsInCart,
                 objectsCollidingScript.itemsInCart
                 );
        }
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
        "focusObjectName;" +
        "focusObjectTag;" +
        "HMDposition;" +
        "HMDrotation;" +
        "HMDvelocity;" +
        "LeftControllerPosition;" +
        "LeftControllerRotation;" +
        "LeftControllerVelocity;" +
        "RightControllerPosition;" +
        "RightControllerRotation;" +
        "RightControllerVelocity;" +
        "numberOfItemsInCart;" + 
        "itemsInCart;" +
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
        string focusObjectName,
        string focusObjectTag,
        Vector3 HMDposition,
        Quaternion HMDrotation,
        Vector3 HMDvelocity,
        Vector3 LeftControllerPosition,
        Quaternion LeftControllerRotation,
        Vector3 LeftControllerVelocity,
        Vector3 RightControllerPosition,
        Quaternion RightControllerRotation,
        Vector3 RightControllerVelocity,
        int numberOfItemsInCart,
        List<string> itemsInCart
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
            focusObjectName + ";" +                         // No need for ToString()
            focusObjectTag + ";" +                          // No need for ToString()
            HMDposition.ToString() + ";" +
            HMDrotation.ToString() + ";" +
            HMDvelocity.ToString() + ";" +
            LeftControllerPosition.ToString() + ";" +
            LeftControllerRotation.ToString() + ";" +
            LeftControllerVelocity.ToString() + ";" +
            RightControllerPosition.ToString() + ";" +
            RightControllerRotation.ToString() + ";" +
            RightControllerVelocity.ToString() + ";" +
            numberOfItemsInCart.ToString() + ";" +
            itemsInCart.ToString() + ";" +
            Environment.NewLine;

        File.AppendAllText(fileName, value);
    }
}
