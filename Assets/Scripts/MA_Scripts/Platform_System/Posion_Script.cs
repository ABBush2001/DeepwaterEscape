using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion_Script : MonoBehaviour
{
    public int poiDmg = 1; // Posion damage
    public float damageOverTime = 0.5f;
    public float poisonLast = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                // playerHealth.TakeDamage(poiDmg);

                StartCoroutine(applyPosion(playerHealth));
            }
        }
    }

    private IEnumerator applyPosion(Player_Health playerH)
    {
        float elapsed = 0f;

        while (elapsed < poisonLast)
        {
            playerH.TakeDamage(poiDmg);
            elapsed += damageOverTime;
            yield return new WaitForSeconds(damageOverTime);
        }

    }
}
