using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countObjectsColliding : MonoBehaviour
{
    public int objectsColliding = 0;
    public List<string> itemsInCart = new List<string>();

    private void OnTriggerEnter(Collider collider)
    {
        if (!itemsInCart.Contains(collider.gameObject.name)) {
            objectsColliding++;
            itemsInCart.Add(collider.gameObject.name);
        }
    }
}
