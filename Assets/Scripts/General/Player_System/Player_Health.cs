using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField] private double currentHealth;
    public float autoHeal = 1f;
    public float autoTime = 1f;

    [Header("Audio")]
    public AudioSource hurtAudioSource;
    public AudioClip hurtClip;

    public Image Healthbar;
    public Image damageScreen;

    [SerializeField]
    private string sceneToLoad;

    private bool ishealing = false;
    public float damageGrace = .2f;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateText();

        //Audio
        if (hurtAudioSource == null)
        {
            hurtAudioSource = gameObject.AddComponent<AudioSource>();
        }

        hurtAudioSource.clip = hurtClip;
        hurtAudioSource.loop = false; 
        hurtAudioSource.playOnAwake = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("You're dead");
            GameOver();
        }

        if (currentHealth < maxHealth && !ishealing)
        {
            StartCoroutine(AHeal());
        }

        if (damageGrace > 0) {
            damageGrace -= Time.deltaTime;
        }
    }

    private IEnumerator AHeal()
    {
        ishealing = true;

        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(autoTime);
            //currentHealth = Mathf.Min(currentHealth + autoHeal, maxHealth);
            currentHealth += autoHeal;
            UpdateText();
        }

        ishealing = false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void TakeDamage(int damage)
    {
        if (damageGrace <= 0f) {
            currentHealth -= damage;
            damageGrace = .2f;
        }
        if (currentHealth <= 0f) {
            GameOver();
        }
        StartCoroutine(DamageScreenDisplay());
        UpdateText();
        if (hurtClip != null && hurtAudioSource != null)
        {
            hurtAudioSource.PlayOneShot(hurtClip);
        }
    }

    void UpdateText()
    {
        if (Healthbar != null)
        {
            //System.Math.Round(currentHealth, 1, 0); //*SPECIFICALLY* System.Math lets you specify decimal places, Mathf does not.
            //Mathf.Round(currentHealth);
            Healthbar.fillAmount = (float)(currentHealth / 100);
            Debug.Log(currentHealth / 100);
            Debug.Log(currentHealth / 100f);
        }
    }

    IEnumerator DamageScreenDisplay()
    {
        damageScreen.enabled = true;
        yield return new WaitForSeconds(0.5f);
        damageScreen.enabled = false;
    }
}
