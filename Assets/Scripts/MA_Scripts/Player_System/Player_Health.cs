using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float autoHeal = 1f;
    public float autoTime = 1f;
    

    public Image Healthbar;
    public Image damageScreen;

    [SerializeField]
    private string sceneToLoad;

    private bool ishealing = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateText();
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
    }

    private IEnumerator AHeal()
    {
        ishealing = true;

        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(autoTime);
            currentHealth = Mathf.Min(currentHealth + autoHeal, maxHealth);
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
        currentHealth -= damage;
        StartCoroutine(DamageScreenDisplay());
        UpdateText();
    }

    void UpdateText()
    {
        if (Healthbar != null)
        {
            Healthbar.fillAmount = currentHealth / 100f;
        }
    }

    IEnumerator DamageScreenDisplay()
    {
        damageScreen.enabled = true;
        yield return new WaitForSeconds(0.5f);
        damageScreen.enabled = false;
    }
}
