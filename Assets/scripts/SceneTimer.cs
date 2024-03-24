using System.Collections;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    // Duration of the scene in seconds
    public float sceneDuration = 122f;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Start coroutine to quit the game after specified duration
        StartCoroutine(QuitGameAfterSeconds(sceneDuration));
    }

    // Coroutine to quit the game after specified duration
    IEnumerator QuitGameAfterSeconds(float sceneDuration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(sceneDuration);

        // Quit the game (only works in Editor mode)
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
