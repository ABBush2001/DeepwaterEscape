using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    //variables
    public GameObject biteSystem;
    public GameObject mainPath;
    public GameObject enemy;
    public GameObject player;
    public TextMeshProUGUI waveText;
    public AudioSource waveRoar;
    public int rotLerpSpeed= 10;

    // wave variables
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
    //private bool attackInProcess;

    public Animator queenAnimator;

    public static bool isMoving = false;

    public bool bossDefeated = false;

    //initialize the attack queue and start the boss fight coroutine 
    void Start()
    {
        //initialize attackQueue to all 0's

        for (int i = 0; i < 5; i++)
        {
            attackQueue[i] = 0;
        }

        //attackInProcess = false;


        StartCoroutine(BossFight());
    }

    //method to do the wave attack
    void WaveAround(GameObject wavePrefab, Vector3 rotationOffset)
    {
        //instantiate the wave and call script to move it
        Quaternion goodEnemyRot = enemy.transform.rotation;
        goodEnemyRot.Set(0f,goodEnemyRot.y,0f,goodEnemyRot.w);

        GameObject temp = Instantiate(wavePrefab);
        temp.transform.SetPositionAndRotation(enemy.transform.position, goodEnemyRot);
        temp.transform.Rotate(rotationOffset);
        
        if (temp.TryGetComponent<Wave_Script>(out var waveScript))
        {
            waveScript.startWave();
        }
        else
        {
            Debug.LogError("Wave prefab is missing the Wave_Script component!");
        }
        Destroy(temp, 1.5f);
    }

    //method to start waves for each wave cylinder
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

    //coroutine to move the boss into position to start the wave
    public IEnumerator MoveEnemyAndStartWave()
    {
        isMoving = true;

        if (enemy == null) yield break;

        if (enemy.TryGetComponent<FollowPath>(out var followPath))
        {
            followPath.enabled = false; // <== Fully disable script
        }

        Vector3 originalPosition = enemy.transform.position;
        float elapsedTime = 0f;
        float moveDuration = 2f;

        
        enemy.transform.LookAt(waveNode.transform.position);
        // Move to wave center
        while (elapsedTime < moveDuration)
        {
            if (enemy == null) yield break;
            enemy.transform.position = Vector3.Lerp(originalPosition, waveNode.transform.position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // This has to be done *very* specifically in order to avoid gimbal lock and successfully remove tilt.
        enemy.transform.position = waveNode.transform.position;
        Quaternion desiredRot = new(0, enemy.transform.rotation.y, 0, enemy.transform.rotation.w);
        enemy.transform.rotation = desiredRot;

        waveText.text = "Wave Incoming";
        yield return new WaitForSeconds(1.5f);

        float remainingTime = 3f;
        waveRoar.Play();

        while (remainingTime > 0)
        {
            waveText.text = $"{remainingTime:F1}s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        waveText.text = "";

        StartWave();

        yield return new WaitForSeconds(3f);

        // Move back
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            if (enemy == null) {
                yield break;
            }
            enemy.transform.position = Vector3.Lerp(waveNode.transform.position, originalPosition, elapsedTime / moveDuration);
            enemy.transform.LookAt(originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = originalPosition;

        // Sync pathing with current position
        if (followPath != null)
        {
            followPath.SnapToClosestNode();
            followPath.enabled = true;
        }

        isMoving = false;
    }



    //method to handle the boss charging at the player
    public IEnumerator ChaseAttack()
    {
        isMoving = true;

        if (enemy == null) yield break;

        FollowPath followPath = enemy.GetComponent<FollowPath>();
        float originalSpeed = 1f;

        //disable boss movement
        if (followPath != null)
        {
            originalSpeed = followPath.moveSpeed;
            followPath.moveSpeed = 0; 
            followPath.enabled = false; 
        }

        enemy.transform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);

        //move boss towards player

        if (player != null)
        {
            Vector3 dashStart = enemy.transform.position;
            Vector3 dashTarget = player.transform.position;
            float dashDuration = 1f;
            float elapsedTime = 0f;

            // Look at the player
            Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
            if (directionToPlayer != Vector3.zero)
            {
                enemy.transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }

            // Dash toward the player
            while (elapsedTime < dashDuration)
            {
                if (enemy == null) yield break;

                enemy.transform.position = Vector3.Lerp(dashStart, dashTarget, elapsedTime / dashDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            enemy.transform.position = dashTarget;
        }

        //move boss back towards its original position        
        if (enemy != null)
        {
            float chaseBackDuration = 0.5f;
            float elapsedReturn = 0f;

            Vector3 chaseStart = enemy.transform.position;

            // Look toward the original position
            Vector3 directionToOriginal = (originalPosition - enemy.transform.position).normalized;
            if (directionToOriginal != Vector3.zero)
            {
                enemy.transform.rotation = Quaternion.LookRotation(directionToOriginal);
            }

            // Chase back to the original position
            while (elapsedReturn < chaseBackDuration)
            {
                if (enemy == null) yield break;

                enemy.transform.position = Vector3.Lerp(chaseStart, originalPosition, elapsedReturn / chaseBackDuration);
                elapsedReturn += Time.deltaTime;
                yield return null;
            }

            enemy.transform.position = originalPosition;
        }

        //reset rotation
        
        if (enemy != null)
        {
            float rotationDuration = 0.5f;
            float elapsedRotation = 0f;
            Quaternion currentRotation = enemy.transform.rotation;

            while (elapsedRotation < rotationDuration)
            {
                if (enemy == null) yield break;

                enemy.transform.rotation = Quaternion.Slerp(currentRotation, originalRotation, elapsedRotation / rotationDuration);
                elapsedRotation += Time.deltaTime;
                yield return null;
            }

            enemy.transform.rotation = originalRotation;
        }

        //reactivate boss movement
        
        if (followPath != null)
        {
            followPath.enabled = true;
            followPath.SnapToClosestNode();
            followPath.moveSpeed = originalSpeed; 
        }

        isMoving = false;
    }


    //method for handling the boss fight
    public IEnumerator BossFight()
    {
        //loop for boss fight
        while (true)
        {
            //initialize queue
            for (int i = 0; i < 5; i++) 
            {
                int temp = Random.Range(1, 101);

                if (temp <= 25) {
                    attackQueue[i] = 2; //temp set to 1, will set back to 2 later
                }
                else if (temp > 25 && temp <= 75) {
                    attackQueue[i] = 1;
                }
                else
                {
                    if (i > 0) {
                        if (attackQueue[i - 1] == 3) {
                            attackQueue[i] = 1;
                        }
                    }
                    else {
                        attackQueue[i] = 3;
                    }
                }
            }

            //get rid of any zero's in the attack queue
            for(int i = 0; i < 5; i++)
            {
                if(attackQueue[i] == 0)
                {
                    int temp = Random.Range(1, 101);

                    if (temp <= 25) { attackQueue[i] = 2; }
                    else
                    {
                        attackQueue[i] = 1;
                    }
                }
            }

            //set first queue item to always be the wave,
            //second to always be dash

            attackQueue[0] = 3;
            attackQueue[1] = 1;

            //attackQueue[0] = 3; //wave
            //attackQueue[1] = 2; //Flash
            //attackQueue[2] = 1; //Dash

            //begin looping through queue
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Item number: " + i);

                //bite path
                if (attackQueue[i] == 1)
                {
                    yield return new WaitForSeconds(15f);
                }
                //flashbang
                else if (attackQueue[i] == 2)
                {
                    Debug.Log("Flashing!");
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    enemy.GetComponent<FlashBang_V1>().StartFlashbang();
                    queenAnimator.SetBool("IsFlashbang", true);
                    yield return new WaitForSeconds(9f);
                    queenAnimator.SetBool("IsFlashbang", false);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
                }
                //wave
                else if (attackQueue[i] == 3)
                {
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 0;
                    StartCoroutine(MoveEnemyAndStartWave());
                    yield return new WaitForSeconds(8f);
                    biteSystem.transform.GetChild(0).gameObject.GetComponent<FollowPath>().moveSpeed = 1;
                }
            }
        }
    }

    public void KillBoss()
    {
        bossDefeated = true;
        StopAllCoroutines();
    }
}


