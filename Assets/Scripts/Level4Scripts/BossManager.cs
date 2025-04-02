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

    public int[] attackQueue = new int[5];
    private bool attackInProcess;

    public Animator queenAnimator;

    
    // Start is called before the first frame update
    void Start()
    {

        //initialize attackQueue to all 0's

        //for (int i = 0; i < 5; i++)
        //{
        //    attackQueue[i] = 0;
        //}

        //attackInProcess = false;


        //StartCoroutine(bossFight());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ChaseAttack()); // Set the target position here
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


    }


    public IEnumerator MoveEnemyAndStartWave()
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

            enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            StartWave();
        }

        // yield return new WaitForSeconds(3);

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



    //public IEnumerator ChaseAttack()
    //{
    //    if (enemy == null)
    //    {
    //        yield break;
    //    }

    //    FollowPath followPath = enemy.GetComponent<FollowPath>();
    //    float originalSpeed = 0;
    //    if (followPath != null)
    //    {
    //        originalSpeed = followPath.moveSpeed;
    //        followPath.moveSpeed = 0;
    //    }

    //    Vector3 originalPosition = enemy.transform.position;
    //    Vector3 wavePosition = waveNode.transform.position;

    //    Quaternion originalRotation = enemy.transform.rotation;

    //    float elapsedTime = 0f;
    //    float moveDuration = 1f;


    //    while (elapsedTime < moveDuration)
    //    {
    //        if (enemy == null)
    //        {
    //            yield break;
    //        }

    //        enemy.transform.position = Vector3.Lerp(originalPosition, wavePosition, elapsedTime / moveDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }


    //    enemy.transform.position = wavePosition;

    //    // Look at the player 
    //    if (player != null)
    //    {

    //        Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
    //        enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    // dash at the player
    //    if (player != null)
    //    {
    //        Vector3 dashTarget = player.transform.position;

    //        elapsedTime = 0f;
    //        float dashDuration = 0.5f;

    //        while (elapsedTime < dashDuration)
    //        {
    //            if (enemy == null)
    //            {
    //                yield break;
    //            }

    //            enemy.transform.position = Vector3.Lerp(wavePosition, dashTarget, elapsedTime / dashDuration);
    //            elapsedTime += Time.deltaTime;
    //            yield return null;
    //        }


    //        enemy.transform.position = dashTarget;
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    elapsedTime = 0f;

    //    while (elapsedTime < moveDuration)
    //    {
    //        if (enemy == null)
    //        {
    //            yield break;
    //        }

    //        enemy.transform.position = Vector3.Lerp(enemy.transform.position, originalPosition, elapsedTime / moveDuration);

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    enemy.transform.position = originalPosition;

    //    float rotationDuration = 0.5f;

    //    while (elapsedTime < rotationDuration)
    //    {
    //        if (enemy == null)
    //        {
    //            yield break;
    //        }

    //        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, originalRotation, elapsedTime / rotationDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    enemy.transform.rotation = originalRotation;

    //    if (enemy != null)
    //    {
    //        enemy.transform.position = originalPosition;

    //        if (followPath != null)
    //        {
    //            followPath.moveSpeed = originalSpeed;
    //        }
    //    }
    //}


    public IEnumerator ChaseAttack()
    {
        if (enemy == null)
        {
            yield break;
        }

        FollowPath followPath = enemy.GetComponent<FollowPath>();
        if (followPath != null)
        {
            followPath.moveSpeed = 0;
        }

        Vector3 originalPosition = enemy.transform.position;
        Vector3 wavePosition = waveNode.transform.position;

        Quaternion originalRotation = enemy.transform.rotation;

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

            // Look at the player 
            if (player != null)
            {

                Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
                enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }

            yield return new WaitForSeconds(0.5f);

            // dash at the player
            if (player != null)
            {
                Vector3 dashTarget = player.transform.position;

                elapsedTime = 0f;
                float dashDuration = 0.5f;

                while (elapsedTime < dashDuration)
                {
                    if (enemy == null)
                    {
                        yield break;
                    }

                    enemy.transform.position = Vector3.Lerp(wavePosition, dashTarget, elapsedTime / dashDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }


                enemy.transform.position = dashTarget;
            }


        }

        // yield return new WaitForSeconds(3);

        elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            if (enemy == null)
            {
                yield break;
            }
            enemy.transform.position = Vector3.Lerp(waveNode.transform.position, originalPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }



        if (enemy != null)

        {
            enemy.transform.position = originalPosition;
            if (followPath != null)
            {
                followPath.moveSpeed = 1;
            }
        }
    }

    //IEnumerator bossFight()
    //{
    //    //loop for boss fight
    //    //NOTE - will later be updated to loop on boss health
    //    while (true)
    //    {
    //        //initialize queue
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //TESTING
    //            //attackQueue[i] = 3;
    //            int temp = Random.Range(1, 101);
    //            if (temp <= 25)
    //            {
    //                attackQueue[i] = 2;
    //            }
    //            else if (temp > 25 && temp <= 75)
    //            {
    //                attackQueue[i] = 3; // change it back to 1
    //            }
    //            else
    //            {
    //                attackQueue[i] = 1; // change it back to 3
    //            }
    //        }

    //        //begin looping through queue
    //        for (int i = 0; i < 5; i++)
    //        {
    //            Debug.Log("Item number: " + i);

    //            //bite path
    //            if (attackQueue[i] == 1)
    //            {

    //                yield return new WaitForSeconds(20);

    //            }
    //            //flashbang
    //            else if (attackQueue[i] == 2)
    //            {

    //                biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
    //                biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
    //                biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
    //                enemy.GetComponent<FlashBang_V1>().startFlashbang();
    //                queenAnimator.SetBool("IsFlashbang", true);
    //                yield return new WaitForSeconds(6);
    //                queenAnimator.SetBool("IsFlashbang", false);
    //                biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
    //                biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
    //                biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
    //            }
    //            //wave
    //            else if (attackQueue[i] == 3)
    //            {
    //                yield return new WaitForSeconds(0.5f);

    //                biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
    //                biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
    //                biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 0;


    //                StartCoroutine(MoveEnemyAndStartWave());
    //                yield return new WaitForSeconds(3);


    //                biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
    //                biteSystem.transform.GetChild(1).gameObject.GetComponent<FollowPath>().moveSpeed = 2;
    //                biteSystem.transform.GetChild(2).gameObject.GetComponent<FollowPath>().moveSpeed = 2;

    //                yield return new WaitForSeconds(0.5f);

    //                player.GetComponent<CommentedThirdPersonController>().SetMovement(true);
    //            }
    //        }
    //    }
    //}
}


