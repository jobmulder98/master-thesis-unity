using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR;
using System.Runtime.InteropServices;
using System;
using System.IO;


namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class SRanipal_Test_4_Combined : MonoBehaviour
            {
                public static string UserID = "Test_4_Combined";
                public static string Path = Directory.GetCurrentDirectory();
                string File_Path = Directory.GetCurrentDirectory() + "\\Eyerecording_" + UserID + ".txt";
                private float time_stamp;
                private int frame;

                // Start is called before the first frame update
                void Start()
                {
                    CreateOutput();
                }

                // Update is called once per frame
                void Update()
                {
                    SRanipal_Eye_v2.GetVerboseData(out VerboseData data);

                    Debug.Log(data.left.pupil_diameter_mm);

                    WriteOutput(data);

                    frame++;
                }

                String GetTimestamp(DateTime value)
                {
                    return value.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                }

                void CreateOutput()
                {
                    string variable =
                    "frame;" +                                         //1
                    "Timestamp;" +                                     //2
                    "openness_L;" +                                    //3
                    "openness_R;" +                                    //4
                    "pupil_diameter_L(mm);" +                          //5
                    "pupil_diameter_R(mm);" +                          //6
                    //"gaze_origin_L.(mm);" +
                    //"gaze_origin_R.(mm);" +
                    //"gaze_direct_L.;" +
                    //"gaze_direct_R.;" +
                    Environment.NewLine;

                    File.AppendAllText("Eyerecording_" + UserID + ".csv", variable);

                }

                void WriteOutput(VerboseData data)
                {
                    //  Convert the measured data to string data to write in a text file.
                    string value =
                        frame.ToString() + ";" +                                    //1
                        GetTimestamp(DateTime.Now) + ";" +                           //2
                        data.left.eye_openness.ToString() + ";" +                   //3
                        data.right.eye_openness.ToString() + ";" +                  //4
                        data.left.pupil_diameter_mm.ToString() + ";" +              //5
                        data.right.pupil_diameter_mm.ToString() + ";" +             //6
                        //data.left.gaze_origin_mm.ToString() + ";" +
                        //data.right.gaze_origin_mm.ToString() + ";" +
                        //data.left.gaze_direction_normalized.ToString() + ";" +
                        //data.right.gaze_direction_normalized.ToString() + ";" +
                        Environment.NewLine;

                    File.AppendAllText("Eyerecording_" + UserID + ".csv", value);
                }
            }

        }
    }
}
