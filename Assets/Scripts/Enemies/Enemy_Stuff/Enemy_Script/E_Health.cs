using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class E_Health : MonoBehaviour
{
    public int EnemyHealth = 100;
    public int EnemyDmg = 25;
    public GameObject parentObj = null; // Destroy parent obj to ensure the soul dies along with the vessel
    public ClamWalker walker;

    const string ANIM_DEAD = "b_isDead";

    // [SerializeField] private Animator EnColl = null;

    //private string sceneToLoad;

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
        if (walker != null) {
            walker.SetDead(true);
        }
        else {
            Debug.LogWarning("Parent obj not assigned", this);
            Destroy(gameObject);
        }
    }

    public void DestroyGameObj()
    {
        if (parentObj != null) { Destroy(parentObj); }
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