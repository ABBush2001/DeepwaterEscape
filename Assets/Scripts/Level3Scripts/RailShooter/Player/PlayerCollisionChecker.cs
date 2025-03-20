using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles checking if the OnRails player collides with an obstacle.
 */
public class PlayerCollisionChecker : MonoBehaviour
{
    [Tooltip("How many seconds until gameover screen")]
    public float countDown = 2;
    [Range(.01f, 1f)]
    [Tooltip("Percentage (0.01 - 1.0) value of how slow the death slowmo should be.")]
    public float deathSlowMo = 0.1f;
    private bool isDead;

    public AudioClip gameOverSound;
    public AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;
        audioSource.clip = gameOverSound;
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            countDown -= (Time.fixedDeltaTime / deathSlowMo); // divide by deathSlowMo as timescale affects 
            if (countDown <= 0) {
                GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isDead)
        {
            isDead = true;
            audioSource.PlayOneShot(gameOverSound);
            Time.timeScale = deathSlowMo; // slowmotion on death
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f; // reset timescale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
