using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay; // Time delay in seconds before the sound starts playing

    private void Start()
    {
        StartCoroutine(PlayDelayedSound());
    }

    private IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
