using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component for playing audio
    public float delay; // Time delay in seconds before the sound starts playing
    public int loopCount; // Number of times to loop the audio (0 means loop indefinitely)

    private int currentLoop = 0; // Current loop count

    // Start is called before the first frame update
    private void Start()
    {
        // Start the coroutine to play the delayed sound
        StartCoroutine(PlayDelayedSound());
    }

    // Coroutine to play the sound after a delay and handle looping
    private IEnumerator PlayDelayedSound()
    {
        // Wait for the specified delay before playing the sound
        yield return new WaitForSeconds(delay);

        // Check if we should loop indefinitely or for a specific number of times
        if (loopCount == 0)
        {
            // Loop indefinitely
            while (true)
            {
                // Play the audio clip
                audioSource.Play();
                // Wait for the duration of the audio clip before playing it again
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }
        else
        {
            // Loop for a specific number of times
            while (currentLoop < loopCount)
            {
                // Play the audio clip
                audioSource.Play();
                // Wait for the duration of the audio clip before playing it again
                yield return new WaitForSeconds(audioSource.clip.length);

                // Increment the loop count
                currentLoop++;

                // Check if we reached the desired loop count
                if (currentLoop >= loopCount)
                {
                    break; // Exit the loop if the desired loop count is reached
                }
            }
        }
    }
}
