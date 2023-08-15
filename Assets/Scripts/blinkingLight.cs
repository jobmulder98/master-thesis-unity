using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkingLight : MonoBehaviour
{
    public Color flashingOffColor = Color.white;
    public Color flashingOnColor = Color.red;
    [Range(0, 10)]
    public float speed = 1;
    public float flashDuration = 10.0f;
    public float pauseDuration = 30.0f;
    public float delayDuration = 10.0f;

    Renderer ren;
    float currentTime = 0.0f;
    bool isFlashing = false;
    bool hasStarted = false;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            currentTime += Time.deltaTime;
            ren.material.color = flashingOffColor;
            if (currentTime >= delayDuration)
            {
                hasStarted = true;
                currentTime = 0.0f;
                isFlashing = true;
            }
        }
        else if (isFlashing)
        {
            currentTime += Time.deltaTime;
            ren.material.color = Color.Lerp(flashingOffColor, flashingOnColor, Mathf.PingPong(Time.time * speed, 1));

            if (currentTime >= flashDuration)
            {
                currentTime = 0.0f;
                isFlashing = false;
                ren.material.color = flashingOffColor;
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime >= pauseDuration)
            {
                currentTime = 0.0f;
                isFlashing = true;
            }
        }
    }
}
