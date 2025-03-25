using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class E_Health : MonoBehaviour
{
    public int EnemyHealth = 100;
    public int EnemyDmg = 25;

    [SerializeField] private Animator EnColl = null;

    private string sceneToLoad;

    //public TextMeshProUGUI Healtext;

    public void DamageOnEnemy(int damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0)
        {
            Defeat();
            // SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void Defeat()
    {
        Destroy(gameObject);
    }

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