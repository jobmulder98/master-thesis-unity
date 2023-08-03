using UnityEngine;

public class AssignTagToChildren : MonoBehaviour
{
    public string tagToAssign;

    void Start()
    {
        gameObject.tag = tagToAssign;
        AssignTagRecursively(transform);
    }

    void AssignTagRecursively(Transform currentTransform)
    {
        currentTransform.gameObject.tag = tagToAssign;

        foreach (Transform child in currentTransform)
        {
            AssignTagRecursively(child);
        }
    }
}