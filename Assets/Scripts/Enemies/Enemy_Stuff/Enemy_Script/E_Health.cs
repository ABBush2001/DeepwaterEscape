using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class E_Health : MonoBehaviour
{
    public int EnemyHealth = 100;
    public int EnemyDmg = 10;
    public GameObject parentObj = null; // Destroy parent obj to ensure the soul dies along with the vessel
    public ClamWalker walker;
    public AudioClip deathClip;
    public AudioSource audioSource;

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
            audioSource.PlayOneShot(deathClip);
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
            Debug.Log("Jelly Hit!");

            if (other.TryGetComponent<Player_Health>(out var playerHealth))
            {
                if (SceneManager.GetActiveScene().name == "5. JellyfishJump")
                {
                    StartCoroutine(jellyDamage(other.gameObject));
                }
                else
                {
                    playerHealth.TakeDamage(EnemyDmg);
                }
            }
        }
    }

    //for use in the jellyfish jump scene
    IEnumerator jellyDamage(GameObject player)
    {
        player.GetComponent<Player_Health>().TakeDamage(EnemyDmg);
        yield return new WaitForSeconds(2f);
    }
}