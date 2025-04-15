using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * This script tracks and updates enemy health. This also includes
 * Enemies dealing damage to the player
*/
public class E_Health : MonoBehaviour
{
    //variables
    public int EnemyHealth = 100;
    public int EnemyDmg = 25;
    public GameObject parentObj = null; // Destroy parent obj to ensure the soul dies along with the vessel

    [SerializeField] private Animator EnColl = null;

    //Method to deal damage to enemies
    public void DamageOnEnemy(int damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0)
        {
            Defeat();
        }
    }

    //Destroy the enemy when health hits zero
    public void Defeat()
    {
        if (parentObj != null) {
            Destroy(parentObj); 
        }
        else {
            Debug.LogWarning("Parent obj not assigned", this);
            Destroy(gameObject);
        }
    }

    //if the player collides with the enemy, deal damage to the player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // More efficient tag comparison
        {
            if (other.TryGetComponent<Player_Health>(out var playerHealth))
            {
                playerHealth.TakeDamage(EnemyDmg);
            }
        }
    }
}