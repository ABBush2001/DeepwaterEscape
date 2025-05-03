using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, enemy.rotation, rotationSpeed * Time.deltaTime);

        //desiredRotation.position = enemy.transform.position;
        //desiredRotation.LookAt(destination);
        //desiredRotation.rotation.Set(0f, desiredRotation.rotation.y, 0f, desiredRotation.rotation.w);
        //enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, desiredRotation.rotation, Time.deltaTime * rotLerpSpeed);
    }
}
