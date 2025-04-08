using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;

    public int numToSpawn;
    public float randomRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startEnemySpawn()
    {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));

        for (int i = 0; i < numToSpawn; i++)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));

            Bounds size = GetComponent<BoxCollider>().bounds;

            GameObject newHazard = GameObject.Instantiate(enemy);
            newHazard.transform.position = new Vector3(Random.Range(this.transform.position.x - randomRange, this.transform.position.x + randomRange), Random.Range(this.transform.position.y - randomRange, this.transform.position.y + randomRange), this.transform.position.z);
        }
    }
}
