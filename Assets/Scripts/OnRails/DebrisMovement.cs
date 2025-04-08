using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 0.0001f;
    public float timeToDespawn = 15;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(debrisTimeOut());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player.position.x, player.position.y, this.transform.position.z), timer);
    }

    IEnumerator debrisTimeOut()
    {
        yield return new WaitForSeconds(timeToDespawn);
        Destroy(this.gameObject);
    }
}
