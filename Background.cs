using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Background : MonoBehaviour {


    public float speed;
    public float Xend;
    public float Xstart;


    private void LateUpdate()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player.GetComponent<Rigidbody2D>().velocity.x != 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (transform.position.x < Xend)
                {
                    Vector2 pos = new Vector2(Xstart, transform.position.y);
                    transform.position = pos;
                }
            }
            
        }
    }
}
