using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Monster {
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        setCurrentHealth();
        setUpHealthBar();
    }

    private void Update()
    {
        if (getCurrentHealth() <= maxHealth/2) {
            anim.SetTrigger("stageTwo");
        }
    }

    public override void attack()
    {
        
        if (playerObject != null)
        {
           
            if (timeAttack <= 0)
            {
                var player = playerObject.GetComponent<Player>();
                if (player.getCurrentHealth() >= 0)
                {
                    player.TakeDamage(attackDamage);
                }
                else
                {
                    player.Die();
                }
                timeAttack = attackSpeed;
            }
            else
            {
                timeAttack -= Time.deltaTime;
            }
        }

    }
    

    
}
