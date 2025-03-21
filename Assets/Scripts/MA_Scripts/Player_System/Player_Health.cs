using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float autoHeal = 1f;
    public float autoTime = 1f;
    public Image healthBar;

    [SerializeField]
    private string sceneToLoad;

    private bool ishealing = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / 100f;
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
            healthBar.fillAmount = currentHealth / 100f;
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
        healthBar.fillAmount = currentHealth / 100f;

    }

}
