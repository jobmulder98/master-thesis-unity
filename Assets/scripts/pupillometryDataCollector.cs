using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class pupillometryDataCollector : MonoBehaviour
{
    public float eyeOpennessLeft;
    public float eyeOpennessRight;
    public float pupilDiameterLeft;
    public float pupilDiameterRight; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SRanipal_Eye_v2.GetVerboseData(out VerboseData pupillometryData);

        eyeOpennessLeft = pupillometryData.left.eye_openness;
        eyeOpennessRight = pupillometryData.right.eye_openness;
        pupilDiameterLeft = pupillometryData.left.pupil_diameter_mm;
        pupilDiameterRight = pupillometryData.right.pupil_diameter_mm;
    }
}
