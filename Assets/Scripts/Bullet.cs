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
    public float BulletlifeSpan = 1.2f;
    public int Damage = 25;

    void Awake()
    {
        // This is to destroy the bullet if the life span end or hit a gameObject
        Destroy(gameObject, BulletlifeSpan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // deal damage to enemy tag

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<E_Health>(out var enemyHealth))
            {
                enemyHealth.DamageOnEnemy(Damage);
            }

            //Destroy(gameObject);
        }


        // deal damage to Boss tag

        if (collision.gameObject.CompareTag("Boss"))
        {
            if (collision.gameObject.TryGetComponent<Boss_health>(out var Bh))
            {
                Bh.DamageOnEnemy(Damage);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Gun") || collision.gameObject.CompareTag("Player"))
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hit enemy trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent<E_Health>(out var enemyHealth))
            {
                enemyHealth.DamageOnEnemy(Damage);
            }

            //Destroy(gameObject);
        }
    }
}
