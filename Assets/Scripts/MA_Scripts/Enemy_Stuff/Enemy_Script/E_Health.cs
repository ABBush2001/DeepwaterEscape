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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageOnEnemy(int damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0)
        {
            defeat();
            // SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void defeat()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // More efficient tag comparison
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(EnemyDmg);

            }
        }
    }
}