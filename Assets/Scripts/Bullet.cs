using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletlifeSpan = 1.2f;
    public int Damage = 25;
    private float timer = 0f;
    private bool isPaused = false;

    void Update()
    {
        if (isPaused) return; // Stop bullets from updating when paused

        timer += Time.deltaTime;
        if (timer >= BulletlifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPaused) return; // Stop collision effects while paused

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<E_Health>(out var enemyHealth))
            {
                enemyHealth.DamageOnEnemy(Damage);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            if (collision.gameObject.TryGetComponent<Boss_health>(out var Bh))
            {
                Bh.DamageOnEnemy(Damage);
            }
            Destroy(gameObject);
        }

        if (!collision.gameObject.CompareTag("Gun"))
        {
            Destroy(gameObject);
        }
    }

    // Subscribe to PauseMenu2 Events
    private void OnEnable()
    {
        PauseMenu2.OnPause += PauseGame;
        PauseMenu2.OnResume += ResumeGame;
    }

    private void OnDisable()
    {
        PauseMenu2.OnPause -= PauseGame;
        PauseMenu2.OnResume -= ResumeGame;
    }

    private void PauseGame()
    {
        isPaused = true;
    }

    private void ResumeGame()
    {
        isPaused = false;
    }
}
