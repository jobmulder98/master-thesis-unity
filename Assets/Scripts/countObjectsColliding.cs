using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countObjectsColliding : MonoBehaviour
{
    public int numberOfItemsInCart = 0;
    public List<string> itemsInCart = new List<string>();
    public float destroyAfterSeconds = 2.5f;

    private void OnTriggerEnter(Collider collider)
    {
        if (!itemsInCart.Contains(collider.gameObject.name)) {
            numberOfItemsInCart++;
            itemsInCart.Add(collider.gameObject.name);

            StartCoroutine(DestroyAfterDelay(collider.gameObject));
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(destroyAfterSeconds);

        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
