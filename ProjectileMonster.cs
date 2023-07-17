using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileMonster : MonoBehaviour
{
    private float timeLife = 1.25f;
    private Rigidbody2D rb;
    public float  damage;
    
    void Start()
    {
        Invoke("DestroyProjectile", timeLife);
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 10;
        float rot = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        GameObject gamePlayer = collider2D.gameObject;
        if (gamePlayer.CompareTag("Player"))
        {
            var player = gamePlayer.GetComponent<Player>();
            if (player != null)
            {
                if (player.getCurrentHealth() >= 0)
                {
                    player.TakeDamage(damage);
                }
                else
                {
                    player.Die();
                }
                DestroyProjectile();
            }
        }else if (gamePlayer.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
