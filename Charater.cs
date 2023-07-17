using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Security.Cryptography;

public abstract class Charater : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    protected float currentHealth;
    public GameObject deathEffect;
    public GameObject explosion;
    public GameObject dealthGameObject;
    protected Rigidbody2D rb;
    protected Animator anim;

    public void setCurrentHealth()
    {
        currentHealth = maxHealth;
    }

    public abstract void attack();
    public void Die()
    {
        
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (dealthGameObject != null) Instantiate(dealthGameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        
        Instantiate(explosion, transform.position, Quaternion.identity);
        currentHealth -= damage;
        upDateHealthBar();
        if (currentHealth <= 0)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("death");
            healthBar.gameObject.SetActive(false);
            Invoke("Die", 5);
        }
    }
    public float getCurrentHealth()
    {
        return currentHealth;
    }

   public void setUpHealthBar()
    {
        
        healthBar.value = 1;
    }
    private void upDateHealthBar()
    {
        if (currentHealth > 0) {
            float val = currentHealth / maxHealth;
            healthBar.value = val;
        }
    }
}
