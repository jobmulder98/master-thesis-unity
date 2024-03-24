using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countObjectsColliding : MonoBehaviour
{
    // Number of items currently in the cart
    public int numberOfItemsInCart = 0;

    // List to keep track of items in the cart
    public List<string> itemsInCart = new List<string>();

    // Time in seconds after which collided objects will be destroyed
    public float destroyAfterSeconds = 2.5f;

    // Called when a collider enters the trigger attached to this GameObject
    private void OnTriggerEnter(Collider collider)
    {
        // Check if the collided object is not already in the cart
        if (!itemsInCart.Contains(collider.gameObject.name))
        {
            // Increment the number of items in the cart
            numberOfItemsInCart++;

            // Add the collided object to the list of items in the cart
            itemsInCart.Add(collider.gameObject.name);

            // Start coroutine to destroy the collided object after a delay
            StartCoroutine(DestroyAfterDelay(collider.gameObject));
        }
    }

    // Coroutine to destroy an object after a specified delay
    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(destroyAfterSeconds);

        // Check if the object still exists (it may have been destroyed by other means)
        if (obj != null)
        {
            // Destroy the object
            Destroy(obj);
        }
    }
}
