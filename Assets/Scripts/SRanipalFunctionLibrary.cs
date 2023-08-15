using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR;


namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class SRanipalFunctionLibrary : MonoBehaviour
            {
                VerboseData data;

                // Start is called before the first frame update
                void Start()
                {
                 
                }

                // Update is called once per frame
                void Update()
                {
                    SRanipal_Eye_v2.GetVerboseData(out data);

                    Debug.Log(data.left.pupil_diameter_mm);
                    
                }
            }

        }
    }
}
