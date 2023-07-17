using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private bool turn = true;
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player") && turn)
        {
            
            GameObject door = GameObject.FindGameObjectWithTag("Door");
            if(door != null)
            {
                Door d = door.GetComponent<Door>();
                d.changeRoom();
                collider2D.gameObject.transform.position = door.transform.position;
                SavePoint.pointPlayer = transform.position;
                turn = false;
            }

        }
    }
}
