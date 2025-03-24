using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script will handle the queue of boss fight attacks in the Arena.
 * Attacks will be activated and deactivated based on the queue. 5 attacks
 * are queued up randomly and played through. Whenever an attack finishes,
 * it sends a signal back to the manager that it has concluded. The probability
 * of any given attack is as follows:
 * Bite - %75 (represented as a 1)
 * Flashbang - %25 (represented as a 2)
 * 
 * NOTE - these numbers will be adjusted as new attacks are created and this
 * script is updated
*/
public class BossManager : MonoBehaviour
{
    public GameObject biteSystem;
    public GameObject mainPath;
    public GameObject enemy;
    public GameObject wave;
    public GameObject player;

    public int[] attackQueue = new int[5];
    private bool attackInProcess;

    // Start is called before the first frame update
    void Start()
    {
        //initialize attackQueue to all 0's
        for (int i = 0; i < 5; i++)
        {
            attackQueue[i] = 0;
        }

        attackInProcess = false;

        //start coroutine
        StartCoroutine(bossFight());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator bossFight()
    {
        //loop for boss fight
        //NOTE - will later be updated to loop on boss health
        while (true)
        {
            //initialize queue
            for (int i = 0; i < 5; i++)
            {
                //TESTING
                //attackQueue[i] = 3;
                int temp = Random.Range(1, 101);
                if (temp <= 25)
                {
                    attackQueue[i] = 2;
                }
                else if(temp > 25 && temp <= 75)
                {
                    attackQueue[i] = 1;
                }
                else
                {
                    attackQueue[i] = 3;
                }
            }

            //begin looping through queue
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Item number: " + i);

                if (attackQueue[i] == 1)
                {

                    yield return new WaitForSeconds(20);

                }
                else if (attackQueue[i] == 2)
                {
                    //flashbangSystem.SetActive(true);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    enemy.GetComponent<FlashBang_V1>().startFlashbang();
                    yield return new WaitForSeconds(6);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
                    biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    //flashbangSystem.SetActive(false);
                }
                else if(attackQueue[i] == 3)
                {
                    mainPath.GetComponent<FollowPath>().setCurrentNode(2);
                    yield return new WaitForSeconds(0.5f);

                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    yield return new WaitForSeconds(3);
                    GameObject temp = Instantiate(wave);
                    temp.transform.SetPositionAndRotation(enemy.transform.position, enemy.transform.rotation);
                    temp.transform.Rotate(new Vector3(0, 0, 90));
                    temp.GetComponent<Wave_Script>().startWave();
                    yield return new WaitForSeconds(1);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
                    biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    Destroy(temp);
                }
            }
        }
    }
}
