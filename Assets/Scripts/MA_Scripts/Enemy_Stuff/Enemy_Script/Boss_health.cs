using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Boss_health : MonoBehaviour
{
    public int BossHealth = 100; // Boss health
    public int BossDmg = 25; // boss damage
    private int currentHealth; // boss current health

    [SerializeField]
    private string sceneToLoad;

    // ui of health
    // public TextMeshProUGUI Healtext;

    public Slider slide;
    public Gradient gradirnt;
    public Image fill;


    // Start is called before the first frame update
    void Start()
    {
        // show boss current health
        currentHealth = BossHealth;

        slide.maxValue = currentHealth;
        slide.value = currentHealth;

        UpdateSliderColor();
        // UpdateText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // if health hit zero then it get destroy
    public void DamageOnEnemy(int damage)
    {
        BossHealth -= damage;

        slide.value = BossHealth;

        UpdateSliderColor();
        // UpdateText();
        if (BossHealth <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
            defeat();
            
        }
    }

    public void defeat()
    {
        Destroy(gameObject);
    }

    // damage the player 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
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

    private void UpdateSliderColor()
    {
        if (fill != null && gradirnt != null)
        {
            fill.color = gradirnt.Evaluate(slide.normalizedValue);
        }


    }
    // ui to show boss health
    //void UpdateText()
    //{
    //    if (Healtext != null)
    //    {
    //        Healtext.text = "Boss " + BossHealth + " / " + currentHealth;
    //    }
    //}
}

