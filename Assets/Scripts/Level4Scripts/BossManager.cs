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
    public GameObject player;

    // wave
    public GameObject wave;
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;

    public GameObject wave4;
    public GameObject wave5;
    public GameObject wave6;
    public GameObject wave7;

    public GameObject waveNode;

    public GameObject digNode;

    public int[] attackQueue = new int[5];
    private bool attackInProcess;

    public Animator queenAnimator;

    
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(MoveEnemyAndChargeAttack()); // Set the target position here
        }
    }


    


    void WaveAround(GameObject wavePrefab, Vector3 rotationOffset)
    {
        GameObject temp = Instantiate(wavePrefab);
        temp.transform.SetPositionAndRotation(enemy.transform.position, enemy.transform.rotation);
        temp.transform.Rotate(rotationOffset);
        Wave_Script waveScript = temp.GetComponent<Wave_Script>();

        if (waveScript != null)
        {
            
            waveScript.startWave();
        }
        else
        {
            Debug.LogError("Wave prefab is missing the Wave_Script component!");
        }
        Destroy(temp, 1.5f);
    }


    void StartWave()
    {
        
        WaveAround(wave, new Vector3(0, 0, 90));
        WaveAround(wave1, new Vector3(0, 90, 90));
        WaveAround(wave2, new Vector3(0, 90, 90));
        WaveAround(wave3, new Vector3(0, 0, 90));

        // the top wave 
        WaveAround(wave4, new Vector3(0, 45, 90));
        WaveAround(wave5, new Vector3(0, -45, 90));

        // the bottom wave 
        WaveAround(wave6, new Vector3(0, -45, 90));
        WaveAround(wave7, new Vector3(0, 45, 90));

        biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
        biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
        biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
    }



    //IEnumerator MoveEnemyAndStartWave()
    //{
    //    // Exit FollowPath by stopping its movement
    //    FollowPath followPath = enemy.GetComponent<FollowPath>();
    //    if (followPath != null)
    //    {
    //        followPath.moveSpeed = 0; // Stop the enemy from following the path
    //    }
    //    // Save the current position of the enemy
    //    Vector3 originalPosition = enemy.transform.position;

    //    float elapsedTime = 0f;
    //    float moveDuration = 1f;

    //    while (elapsedTime < moveDuration)
    //    {
    //        enemy.transform.position = Vector3.Lerp(originalPosition, waveNode.transform.position, (elapsedTime / moveDuration));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    enemy.transform.position = waveNode.transform.position; // Ensure it ends at the target position

    //    // Start the wave after moving forward
    //    StartWave();

    //    // Move the enemy back to the original position 
    //    Vector3 targetPositionBack = originalPosition;
    //    elapsedTime = 0f;

    //    while (elapsedTime < moveDuration)
    //    {
    //        enemy.transform.position = Vector3.Lerp(waveNode.transform.position, targetPositionBack, (elapsedTime / moveDuration));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // Ensure it ends at the original position
    //    enemy.transform.position = targetPositionBack; 

    //    // Re-enter FollowPath and resume movement
    //    if (followPath != null)
    //    {
    //        followPath.moveSpeed = 1; 
    //    }
    //}


    IEnumerator MoveEnemyAndStartWave()
    {
        if(enemy == null)
        {
            yield break;
        }

        FollowPath followPath = enemy.GetComponent<FollowPath>();
        if (followPath != null)
        {
            followPath.moveSpeed = 0;
        }

        Vector3 originalPosition = enemy.transform.position;

        float elapsedTime = 0f;
        float moveDuration = 1f;

        while (elapsedTime < moveDuration)
        {
            if (enemy == null)
            {
                yield break;
            }
            enemy.transform.position = Vector3.Lerp(originalPosition, waveNode.transform.position, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (enemy != null)
        {
            enemy.transform.position = waveNode.transform.position;

            StartWave();

            elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                if(enemy == null)
                {
                    yield break;
                }

                enemy.transform.position = Vector3.Lerp(waveNode.transform.position, originalPosition, (elapsedTime / moveDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if(enemy != null)
            {
                enemy.transform.position = originalPosition;

                if (followPath != null)
                {
                    followPath.moveSpeed = 1;
                }
            }
        }
    }


    // This the charge attack where it will look at the player the charge
    IEnumerator MoveEnemyAndChargeAttack()
    {
        // Exit FollowPath by stopping its movement
        FollowPath followPath = enemy.GetComponent<FollowPath>();
        if (followPath != null)
        {
            followPath.moveSpeed = 0; // Stop the enemy from following the path
        }
        // Save the current position of the enemy
        Vector3 originalPosition = enemy.transform.position;

        float elapsedTime = 0f;
        float moveDuration = 1f;

        transform.LookAt(player.transform);

        while (elapsedTime < moveDuration)
        {
            enemy.transform.position = Vector3.Lerp(originalPosition, player.transform.position, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        enemy.transform.position = player.transform.position;

        Vector3 targetPositionBack = originalPosition;
        elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            enemy.transform.position = Vector3.Lerp(digNode.transform.position, targetPositionBack, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends at the original position
        enemy.transform.position = targetPositionBack;

        // Re - enter FollowPath and resume movement
        if (followPath != null)
        {
            followPath.moveSpeed = 1;
        }
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
                    attackQueue[i] = 3; // change it back to 1
                }
                else
                {
                    attackQueue[i] = 1; // change it back to 3
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
                    queenAnimator.SetBool("IsFlashbang", true);
                    yield return new WaitForSeconds(6);
                    queenAnimator.SetBool("IsFlashbang", false);
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

                    queenAnimator.SetBool("IsShouting", true);
                    yield return new WaitForSeconds(5);

                    //GameObject temp = Instantiate(wave);
                    //temp.transform.SetPositionAndRotation(enemy.transform.position, enemy.transform.rotation);
                    //temp.transform.Rotate(new Vector3(0, 0, 90));

                    //GameObject temp2 = Instantiate(wave1);
                    //temp.transform.SetPositionAndRotation(enemy.transform.position, enemy.transform.rotation);
                    //temp.transform.Rotate(new Vector3(0, -90, 90));
                    //Wave_Script waveScript = temp.GetComponent<Wave_Script>();

                    //if(waveScript != null)
                    //{
                    //    waveScript.startWave();
                    //}
                    //else
                    //{
                    //    Debug.LogError("Wave prefab is missing the Wave_Script component!");
                    //}

                    // StartWave();

                    StartCoroutine(MoveEnemyAndStartWave());

                    queenAnimator.SetBool("IsShouting", false);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
                    biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
                    
                }
            }
        }
    }
}


