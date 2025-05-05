using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
 * This script handles boss health. This includes the boss dealing
 * damage to the player
*/
public class Boss_health : MonoBehaviour
{
    //variables
    public int BossHealth = 100; // Boss health
    public int BossDmg = 25; // boss damage
    private int currentHealth; // boss current health

    [SerializeField]
    private string sceneToLoad;

    public Slider slide;
    public Gradient gradirnt;
    public Image fill;

    public GameObject cutscene2;

    public GameObject enemyParentObj;

    //Set the initial health and slider values
    void Start()
    {
        // show boss current health
        currentHealth = BossHealth;

        slide.maxValue = currentHealth;
        slide.value = currentHealth;

        UpdateSliderColor();
    }

    // if health hit zero then it get destroy
    public void DamageOnEnemy(int damage)
    {
        BossHealth -= damage;

        slide.value = BossHealth;

        UpdateSliderColor();
        if (BossHealth <= 0)
        {
            //SceneManager.LoadScene(sceneToLoad);
            cutscene2.GetComponent<ClosingBossCutscene>().BeginCutscene();
            Defeat();
            
        }
    }

    //destroy enemy when defeated
    public void Defeat()
    {
        GameObject.Find("BossManager").GetComponent<BossManager>().bossDefeated = true;
        Destroy(enemyParentObj, .03f); // kill the whole thing
        Destroy(gameObject);
    }

    // damage the player and damage from bullets
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Player_Health>(out var playerHealth))
            {
                playerHealth.TakeDamage(BossDmg);
            }
        }

        if(other.CompareTag("Bullet"))
        {
            DamageOnEnemy(25);
            Destroy(other.gameObject);
        }
    }

    //Set the color of the health slider
    private void UpdateSliderColor()
    {
        if (fill != null && gradirnt != null)
        {
            fill.color = gradirnt.Evaluate(slide.normalizedValue);
        }
    }
}

