using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script handles the clam 
 */

public class ClamWalker : MonoBehaviour
{
    public NavMeshAgent clamNavAgent;
    public Transform playerPos;
    private Vector3 clamJumpTarget;
    private Transform clamTransform;

    private Player_Health playerHealth;

    private bool hasSeenPlayer = false;
    private bool hasDeBurrowed = false;
    private bool isJumping = false;
    private bool canHurt = false;

    [Tooltip("Time it takes for clam to move out of the ground and do first jump. May be replaced with animation events")]
    public float deBurrowTime;
    [Tooltip("Cooldown between jumps")]
    public float jumpCooldown;
    private float curJumpCooldown;
    [Tooltip("The maximum horizontal distance the clam can jump to.")]
    public float maxJumpDistance;
    [Tooltip("The amount of damage the clam does.")]
    public int damage;

    //[Tooltip("Whether or not to override the scripable object data and use custom values.")]
    //public bool overrideScriptObjData;

    [Header("Patrol Variables")]
    [Tooltip("Does the clam patrol? Enables below variables to work. You shouldn't touch this.")]
    public bool patrols;
    [Tooltip("'Waypoints' goes here, but you probably shouldn't touch this.")]
    public Transform waypointList;
    private Transform[] waypoints;
    private int waypointIndex = 0;
    private Vector3 target;

    public ClamScriptObj clamData;


    private void Start()
    {
        clamTransform = this.GetComponent<Transform>();
        playerHealth = GetComponentInParent<ClamPlayerHealthRef>().GetPlayerHealth();
        if (patrols) {
            waypoints = new Transform[waypointList.childCount]; // actually intialize array with size of waypointlist
            foreach (Transform t in waypointList) // Get each waypoint in waypointList automagically
            {
                waypoints[waypointIndex] = t;
                waypointIndex++;
            }
            waypointIndex = 0; // We're using this for later, keep it around at 0
            UpdateClamPatrolDest();
        }
    }

    private void FixedUpdate()
    {
        if (patrols && !hasSeenPlayer)
        {
            if (Vector3.Distance(clamTransform.position, target) < 2f) {
                UpdateClamPatrolDest();
                IterwateWaypointIndex();
            }
        }

        else if (hasDeBurrowed && hasSeenPlayer) // Has clam deburrowed?
        {
            ClamJumpThinklogic();
        }

        else if (hasSeenPlayer) // Hasn't deburrowed, has it seen the player? If so, run the deburrow timer and look at the player.
        { 
            clamTransform.LookAt(playerPos);

            if (deBurrowTime > 0) {
                deBurrowTime -= Time.fixedDeltaTime;
            }
            else {
                hasDeBurrowed = true;
            }
        }
    }

    // Don't use Update() since we don't need to calculate AI stuff every single frame, especially for a mob enemy.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasSeenPlayer)
            {
                playerPos = other.transform;    
                clamNavAgent.baseOffset += 1; // Offset is just until animations get in
                clamNavAgent.destination = transform.position; // stop patrol when player is detected

            }
            hasSeenPlayer = true;
        }
    }

    public ClamWalker(float deBurrowTime, float jumpCooldown, float maxJumpDistance, int damage, bool patrols)
    {
        this.deBurrowTime = deBurrowTime;
        this.jumpCooldown = jumpCooldown;
        this.maxJumpDistance = maxJumpDistance;
        this.damage = damage;
        this.patrols = patrols;
    }

    protected void ClamJumpThinklogic()
    {
        if (curJumpCooldown <= 0) { // Is the jump cooldown done? If so, call ClamJump()
            ClamJump();
        }

        else // If jump cooldown isn't done, check velocity to see if clam is done jumping.
        {
            //if (clamNavAgent.velocity.sqrMagnitude <= 0.1f)
            if (clamNavAgent.remainingDistance <= 1f)
            {
                isJumping = false;
                canHurt = false;
            }

            if (!isJumping) // If not jumping, decrement jump cooldown counter and look at the player.
            {
                curJumpCooldown -= Time.fixedDeltaTime;
                clamTransform.LookAt(playerPos);
            }
        }
    }

    private void ClamJump()
    {
        clamJumpTarget = Vector3.MoveTowards(clamTransform.position, playerPos.position, maxJumpDistance);
        clamJumpTarget.y = 0;
        clamNavAgent.destination = clamJumpTarget;
        //clamNavAgent.destination.Set(clamNavAgent.destination.x, 0, clamNavAgent.destination.z);
        curJumpCooldown = jumpCooldown;
        isJumping = true; // isJumping only exists to make code relating to looking at the player easier to understand
        canHurt = true; // Clam only hurts player when jumping.
    }

    public void AttemptHurt()
    {
        if (canHurt)
        {
            playerHealth.TakeDamage(damage);
            canHurt = false; // Make sure they can't get hurt multi ple times in one jump.
        }
    }

    private void UpdateClamPatrolDest()
    {
        target = waypoints[waypointIndex].position;
        clamNavAgent.SetDestination(target);
    }

    void IterwateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }
}

struct Stinky { 

}

/*
 * Clam patrol values;
 * Base offset: 0
 * Speed: 15
 * Angular Speed: 360
 * Acceleration: 20
 * Stop Dist.: 0
 * Auto Brake: False
 * 
 * Radius: 2.75
 * Height: 2.85
 * No Quality
 * Priority: 50
 */