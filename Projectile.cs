using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;
    /*
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null && hitInfo.collider is BoxCollider2D) {
            if (hitInfo.collider.CompareTag("Monster")) {
                hitInfo.collider.GetComponent<Monster>().TakeDamage(damage);
            }
            DestroyProjectile();
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
       // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }*/
    private float timeLife = 1.25f;

    void Start()
    {
        Invoke("DestroyProjectile", timeLife);
       
    }
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D is BoxCollider2D)
        {
            GameObject gamePlayer = collider2D.gameObject;
            if (gamePlayer.CompareTag("Monster"))
            {
                var player = gamePlayer.GetComponent<Monster>();
                if (player != null)
                {
                    if (player.getCurrentHealth() >= 0)
                    {
                        player.TakeDamage(50f);
                    }
                    else
                    {
                        player.Die();
                    }
                    DestroyProjectile();
                }
            }
        }
        else
        {
            GameObject gamePlayer = collider2D.gameObject;
            if (gamePlayer.CompareTag("Wall")) DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
