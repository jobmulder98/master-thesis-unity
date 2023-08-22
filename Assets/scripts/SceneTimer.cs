using System.Collections;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    public float sceneDuration = 125f;

    private void Start()
    {
        StartCoroutine(QuitGameAfterSeconds(sceneDuration));
    }

    IEnumerator QuitGameAfterSeconds(float sceneDuration)
    {
        yield return new WaitForSeconds(sceneDuration);
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
}
