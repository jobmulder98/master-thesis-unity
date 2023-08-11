using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Tobii.XR;
using System.IO;
using System;


public class FixationOnObject : MonoBehaviour, IGazeFocusable
{
    private float timeStamp;
    private int frame;
    private bool _hasFocus;
    private int _numberFocusedOnObject;
    private float _totalGazeTime;
 
    void Start()
    {
        //fileName = Directory.GetCurrentDirectory() + "\\" + dataDirectory + "\\" + gameObject.name.ToString() + condition + "_" + userID.ToString() + ".csv";
        //CreateOutput();
    }

    void Update()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        timeStamp = eyeTrackingData.Timestamp;
        
        if (_hasFocus)
        {
            //WriteOutput();
            _totalGazeTime += Time.deltaTime;
        }
            
        frame++;
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        _hasFocus = hasFocus;

        if (hasFocus)
        {
            _numberFocusedOnObject += 1;
        }
        else
        {
            _totalGazeTime = 0;
        }
    }

    String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss.ffff");
    }

    //void CreateOutput()
    //{
    //    string variable =
    //    "objectName;" +
    //    "timesFocusedOnObject;" + 
    //    "frame;" +                                         // frame number in session
    //    "Timestamp;" +                                     // timestamp in session
    //    Environment.NewLine;

    //    File.AppendAllText(fileName, variable);
    //}

    //void WriteOutput()
    //{
    //    string value =
    //        gameObject.name + ";" +
    //        _numberFocusedOnObject.ToString() + ";" +
    //        _totalGazeTime.ToString() + ";" +
    //        frame.ToString() + ";" +                                 
    //        //GetTimestamp(DateTime.Now) + ";" + 
    //        timeStamp + ";" +
    //        Environment.NewLine;

    //    File.AppendAllText(fileName, value);
    //}
}

