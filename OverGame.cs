using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverGame : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            GameObject gameObject = collider2D.gameObject;
            Destroy(gameObject);
        }
    }
}
