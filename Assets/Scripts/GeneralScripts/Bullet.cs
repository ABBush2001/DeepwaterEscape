using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the behavior for bullets for the gun prefab.
 * It creates a bullet lifespan, which ensures the bullet will
 * despawn after traveling a given distance. It also will destroy the
 * bullet when it collides with an object.
*/
public class Bullet : MonoBehaviour
{
    // The life span of the bullet
    public float BulletlifeSpan = 3;
    public int Damage = 25;

    void Awake()
    {
        // This is to destroy the bullet if the life span end or hit a gameObject
        Destroy(gameObject, BulletlifeSpan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // deal damage to enemy tag

        if (collision.gameObject.tag == "Enemy")
        {
            E_Health enemyHealth = collision.gameObject.GetComponent<E_Health>();
            if (enemyHealth != null)
            {
                enemyHealth.DamageOnEnemy(Damage);
            }

            Destroy(gameObject);
        }

        

        // deal damage to Boss tag

        if (collision.gameObject.tag == "Boss")
        {
            Boss_health Bh = collision.gameObject.GetComponent<Boss_health>();
            if (Bh != null)
            {
                Bh.DamageOnEnemy(Damage);
            }

            Destroy(gameObject);
        }

        
    }
}
