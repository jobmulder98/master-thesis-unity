using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Tobii.XR;
using System.IO;
using UnityEngine.XR.Interaction.Toolkit;
using ViveSR.anipal.Eye;

public class DataCollector : MonoBehaviour
{
    public bool writeDataToFile = false;

    public int userID = 0;
    public int condition = 1;
    public string dataDirectory = "Data";
    private string fileName;
    private int frame;
    DateTime timeStamp;

    private string focusObjectName;
    private string focusObjectTag;

    Vector3 rayOrigin;
    Vector3 rayDirection;
    float convergenceDistance;

    string itemsInCartReformatted;

    public Rigidbody RigidBodyHMD;
    public Rigidbody RigidBodyLeftController;
    public Rigidbody RigidBodyRightController;

    private XRGrabInteractable grabbedObject;
    public bool isGrabbing;

    public countObjectsColliding objectsCollidingScript;
    public ControllerGrabLogger controllerGrabLogger;
    public pupillometryDataCollector pupillometryDataCollector;

    // Start is called before the first frame update
    void Awake()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
    }

    void Start()
    {
        fileName = Path.Combine(Directory.GetCurrentDirectory(), dataDirectory, userID.ToString(), "datafile_" + "C" + condition + ".csv");
        grabbedObject = GetComponent<XRGrabInteractable>();
        CreateOutput();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 HMDPosition = RigidBodyHMD.position;
        Quaternion HMDRotation = RigidBodyHMD.rotation;

        Vector3 LeftControllerPosition = RigidBodyLeftController.position;
        Quaternion LeftControllerRotation = RigidBodyLeftController.rotation;

        Vector3 RightControllerPosition = RigidBodyRightController.position;
        Quaternion RightControllerRotation = RigidBodyRightController.rotation;

        timeStamp = DateTime.Now;
        string formattedTimestampDatetime = timeStamp.ToString("yyyy-MM-dd HH:mm:ss.ffff");

        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

        float timeStampTrackingData = eyeTrackingData.Timestamp;

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
            focusObjectTag = "notAssigned";
        }

        frame++;
        List<string> items = objectsCollidingScript.itemsInCart;
        itemsInCartReformatted = string.Join(",", items.ToArray());

        if (writeDataToFile)
        {
            WriteOutput(
                 userID,
                 condition,
                 formattedTimestampDatetime,
                 timeStampTrackingData,
                 frame,
                 rayOrigin,
                 rayDirection,
                 convergenceDistance,
                 isLeftEyeBlinking,
                 isRightEyeBlinking,
                 pupillometryDataCollector.eyeOpennessLeft,
                 pupillometryDataCollector.eyeOpennessRight,
                 pupillometryDataCollector.pupilDiameterLeft,
                 pupillometryDataCollector.pupilDiameterRight,
                 eyesDirection,
                 focusObjectName,
                 focusObjectTag,
                 HMDPosition,
                 HMDRotation,
                 LeftControllerPosition,
                 LeftControllerRotation,
                 RightControllerPosition,
                 RightControllerRotation,
                 objectsCollidingScript.numberOfItemsInCart,
                 itemsInCartReformatted,
                 controllerGrabLogger.isGrabbing,
                 controllerGrabLogger.grabbedObjectName
                 );
        }
    }

    void CreateOutput()
    {
        string variable =
        "userID;" +
        "condition;" +
        "timeStampDatetime;" +
        "timeStampTrackingData;" +
        "frame;" +
        "rayOrigin;" +
        "rayDirection;" +
        "convergenceDistance;" +
        "isLeftEyeBlinking;" +
        "isRightEyeBlinking;" +
        "eyeOpennessLeft;" +
        "eyeOpennessRight;" +
        "pupilDiameterLeft;" +
        "pupilDiameterRight;" +
        "eyesDirection;" +
        "focusObjectName;" +
        "focusObjectTag;" +
        "HMDposition;" +
        "HMDrotation;" +
        "LeftControllerPosition;" +
        "LeftControllerRotation;" +
        "RightControllerPosition;" +
        "RightControllerRotation;" +
        "numberOfItemsInCart;" + 
        "itemsInCart;" +
        "isGrabbing;" +
        "grabbedObjectName" +
        Environment.NewLine;

        if (!File.Exists(fileName))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
        }

        File.AppendAllText(fileName, variable);
    }

    void WriteOutput(
        int userID,
        int condition,
        string timeStampDatetime,
        float timeStampTrackingData,
        int frame,
        Vector3 rayOrigin,
        Vector3 rayDirection,
        float convergenceDistance,
        bool isLeftEyeBlinking,
        bool isRightEyeBlinking,
        float eyeOpennessLeft,
        float eyeOpennessRight,
        float pupilDiameterLeft,
        float pupilDiameterRight,
        Vector3 eyesDirection,
        string focusObjectName,
        string focusObjectTag,
        Vector3 HMDposition,
        Quaternion HMDrotation,
        Vector3 LeftControllerPosition,
        Quaternion LeftControllerRotation,
        Vector3 RightControllerPosition,
        Quaternion RightControllerRotation,
        int numberOfItemsInCart,
        string itemsInCart,
        bool isGrabbing, 
        string grabbedObjectName
        )
    {
        string value =
            userID.ToString() + ";" +
            condition.ToString() + ";" +
            timeStampDatetime + ";" +
            timeStampTrackingData.ToString() + ";" +
            frame.ToString() + ";" +
            rayOrigin.ToString() + ";" +
            rayDirection.ToString() + ";" +
            convergenceDistance.ToString() + ";" +
            isLeftEyeBlinking.ToString() + ";" +
            isRightEyeBlinking.ToString() + ";" +
            eyeOpennessLeft.ToString() + ";" +
            eyeOpennessRight.ToString() + ";" +
            pupilDiameterLeft.ToString() + ";" +
            pupilDiameterRight.ToString() + ";" +
            eyesDirection.ToString() + ";" +
            focusObjectName + ";" +                         // No need for ToString()
            focusObjectTag + ";" +                          // No need for ToString()
            HMDposition.ToString() + ";" +
            HMDrotation.ToString() + ";" +
            LeftControllerPosition.ToString() + ";" +
            LeftControllerRotation.ToString() + ";" +
            RightControllerPosition.ToString() + ";" +
            RightControllerRotation.ToString() + ";" +
            numberOfItemsInCart.ToString() + ";" +
            itemsInCart + ";" +
            isGrabbing.ToString() + ";" + 
            grabbedObjectName + ";" +
            Environment.NewLine;

        File.AppendAllText(fileName, value);
    }
}