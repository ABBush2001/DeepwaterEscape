using System.Collections;
using UnityEngine;

public class LerpBossMovement : MonoBehaviour
{
    public Transform enemy;
    public float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null) {
            Debug.LogAssertion("Enemy gameobj not hooked up to LerpBossMovement", gameObject);
        }
        else { StartCoroutine(Lerp()); }
    }

    
    IEnumerator Lerp()
    {
        while (true)
        {
            transform.SetPositionAndRotation(enemy.position, Quaternion.Slerp(transform.rotation, enemy.rotation, rotationSpeed * Time.deltaTime));
            yield return null;
        }
    }
}
