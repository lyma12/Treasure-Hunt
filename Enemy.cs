using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        setCurrentHealth();
    }

    // Update is called once per frame
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
