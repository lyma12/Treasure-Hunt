using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public abstract class Monster : Charater
{
    
    protected bool chase = true;
    protected GameObject playerObject;
    protected float timeAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnCollisionStay2D(Collision2D collision2D)
    {
        
        GameObject gameObject = collision2D.gameObject;
        if (gameObject.CompareTag("Player"))
        {
            chase = false;
            playerObject = gameObject;
            attack();
        }
        
    }
    
    protected virtual void OnTriggerStay2D(Collider2D collider2D)
    {
        Animator anim = GetComponent<Animator>();
            GameObject gameObject = collider2D.gameObject;
            // kiem tra co phai charater khong
            if (gameObject.CompareTag("Player") && chase)
            {
            anim.SetTrigger("run");
            
            
            this.transform.position = Vector2.MoveTowards(this.transform.position, gameObject.transform.position, speed * Time.deltaTime);
            }
        
    }
    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        chase = true;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("idle");
        anim.ResetTrigger("run");
    }

}
