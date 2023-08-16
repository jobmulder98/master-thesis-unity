using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices; 
using ViveSR.anipal.Eye;

public class pupillometryDataCollector : MonoBehaviour
{
    public bool writeDataToFile = true;

    public int userID = 0;
    public int condition = 1;
    public string dataDirectory = "Data";
    private string fileName;
    private int frame;
    DateTime timeStamp;

    public float eyeOpennessLeft;
    public float eyeOpennessRight;
    public float pupilDiameterLeft;
    public float pupilDiameterRight; 

    // Start is called before the first frame update
    void Start()
    {
        fileName = Path.Combine(Directory.GetCurrentDirectory(), dataDirectory, userID.ToString(), "pupillometry_" + "C" + condition + ".csv");
        CreateOutput();
        SRanipal_Eye_v2.GetVerboseData(out VerboseData pupillometryData);
    }

    // Update is called once per frame
    void Update()
    {
        SRanipal_Eye_v2.GetVerboseData(out VerboseData pupillometryData);

        Debug.Log(pupillometryData.left.pupil_diameter_mm);

        eyeOpennessLeft = pupillometryData.left.eye_openness;
        eyeOpennessRight = pupillometryData.right.eye_openness;
        pupilDiameterLeft = pupillometryData.left.pupil_diameter_mm;
        pupilDiameterRight = pupillometryData.right.pupil_diameter_mm;

        timeStamp = DateTime.Now;
        string formattedTimestampDatetime = timeStamp.ToString("yyyy-MM-dd HH:mm:ss.ffff");
        frame++;

        if (writeDataToFile)
            {
                WriteOutput(
                    userID,
                    condition,
                    formattedTimestampDatetime,
                    frame,
                    eyeOpennessLeft,
                    eyeOpennessRight,
                    pupilDiameterLeft,
                    pupilDiameterRight
                    );
            }
    }

    void CreateOutput()
    {
        string variable =
        "userID;" +
        "condition;" +
        "timeStampDatetime;" +
        "frame;" +
        "eyeOpennessLeft;" +
        "eyeOpennessRight;" +
        "pupilDiameterLeft;" +
        "pupilDiameterRight;" +
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
        int frame,
        float eyeOpennessLeft,
        float eyeOpennessRight,
        float pupilDiameterLeft,
        float pupilDiameterRight
        )
    {
        string value =
            userID.ToString() + ";" +
            condition.ToString() + ";" +
            timeStampDatetime + ";" +
            frame.ToString() + ";" +
            eyeOpennessLeft.ToString() + ";" +
            eyeOpennessRight.ToString() + ";" +
            pupilDiameterLeft.ToString() + ";" +
            pupilDiameterRight.ToString() + ";" +
            Environment.NewLine;

        File.AppendAllText(fileName, value);
    }
}


