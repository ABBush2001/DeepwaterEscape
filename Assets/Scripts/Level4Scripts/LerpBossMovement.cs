using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBossMovement : MonoBehaviour
{
    public Transform desiredRotation;
    public Transform enemy;

    private GameObject enemyModel;
    // Start is called before the first frame update
    void Start()
    {
        enemyModel = this.gameObject;
        if (enemy == null) {
            Debug.LogAssertion("Enemy gameobj not hooked up to LerpBossMovement",this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //desiredRotation.position = enemy.transform.position;
        //desiredRotation.LookAt(destination);
        //desiredRotation.rotation.Set(0f, desiredRotation.rotation.y, 0f, desiredRotation.rotation.w);
        //enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, desiredRotation.rotation, Time.deltaTime * rotLerpSpeed);
    }
}
