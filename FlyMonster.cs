using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonster : Monster
{
    public GameObject projectile;
    private float offset = -90f;
    public LayerMask whatIsSolid;
    public Transform Weapon;
    public ProjectileMonster punk;
    void Start()
    {
        setCurrentHealth();
    }

    public override void attack()
    {
        Vector3 difference = playerObject.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var rotationPunk = Quaternion.Euler(0f, 0f, rotZ);
        ProjectileMonster t = Instantiate(punk, Weapon.transform.position, rotationPunk);
        t.damage = attackDamage;
    }
    private void OnTriggerStay2D(Collider2D collider2D)
       {
           playerObject = collider2D.gameObject;
           if (playerObject.CompareTag("Player"))
           {
               if (timeAttack <= 0.1)
               {
                   attack();
                   timeAttack = attackSpeed;
                   this.transform.position = Vector2.MoveTowards(this.transform.position, playerObject.transform.position, speed * Time.deltaTime);
               }
               else
               {
                   timeAttack -= Time.deltaTime;
               }
           }
       }




}
