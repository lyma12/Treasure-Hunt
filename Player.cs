using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Charater
{
    private List<string> keys = new List<string>();
    private DataJson data;
    private int coins;
    private int stars;
    
    public float jumpHeight;
    
    private Vector2 moveVelocity;
    private bool finishGame = false;
    // Start is called before the first frame update
    void Start()
    {
        setCurrentHealth();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
        data = GameData.loadData("data.json");
      
        //data = DataStatic.data;
        coins = data.Coins;
        stars = data.Stars;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        moveVelocity = moveInput * speed;
       // UnityEngine.Debug.Log(moveInput.ToString());
        if (moveInput != Vector2.zero)
        {
            anim.SetTrigger("run");
        }
        else
        {
            anim.SetTrigger("idle");
        }
    }
    public void setCoins(int coins)
    {
        this.coins += coins;
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Finish"))
        {
            data.Coins = coins;
            data.Stars = stars;
            GameData.saveData(data);
            //DataStatic.data = data;
            finishGame = true;
        }
        else if (collider2D.gameObject.CompareTag("Coin"))
        {
            GameObject gameObject = collider2D.gameObject;
            coins += 30;
            Destroy(gameObject);
        }
        else if (collider2D.gameObject.CompareTag("Star"))
        {
            stars += 1;
            GameObject gameObject = collider2D.gameObject;
            Destroy(gameObject);
        }
        else if (collider2D.gameObject.CompareTag("Health"))
        {
            if (currentHealth + 10 <= maxHealth) currentHealth += 10;
            else currentHealth = maxHealth;
            TakeDamage(0f);
            GameObject gameObject = collider2D.gameObject;
            Destroy(gameObject);
        }
        else if (collider2D.gameObject.CompareTag("Key"))
        {
            Key k = collider2D.gameObject.GetComponent<Key>();
            keys.Add(k.getCodeKey());
            GameObject gameObject = collider2D.gameObject;
            Destroy(gameObject);
        }
    }
    public void jump()
    {
        if(rb.velocity.y > 0f) rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y * 0.5f);
        else rb.velocity = new Vector2(5f, jumpHeight);

    }
    public void run(float horizontal)
    {
        anim.SetTrigger("run");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    public List<string> getKeys()
    {
        return keys;
    }
    public override void attack()
    {

    }
    public void exceptCoins(int exCoins)
    {
        coins -= exCoins;
        data.Coins = coins;
        DataStatic.data = data;
        GameData.saveData(data);

    }
    public void exceptStars(int exStars)
    {
        stars -= exStars;
        data.Stars = stars;
        GameData.saveData(data);
    }
    public int getStars()
    {
        return stars;
    }
    public int getCoins()
    {
        return coins;
    }
    public bool getFinishGame()
    {
        return finishGame;
    }
}
