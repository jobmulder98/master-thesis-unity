using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay; // Time delay in seconds before the sound starts playing
    public int loopCount; // Number of times to loop the audio (0 means loop indefinitely)

    private int currentLoop = 0; // Current loop count

    private void Start()
    {
        StartCoroutine(PlayDelayedSound());
    }

    private IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(delay);

        // Check if we should loop indefinitely or for a specific number of times
        if (loopCount == 0)
        {
            while (true)
            {
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }
        else
        {
            while (currentLoop < loopCount)
            {
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);

                currentLoop++;

                // Check if we reached the desired loop count
                if (currentLoop >= loopCount)
                {
                    break;
                }
            }
        }
    }
}