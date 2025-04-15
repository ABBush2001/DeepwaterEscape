using UnityEngine;
using UnityEngine.AI;

/*
 * This script handles the clam 
 */

public class ClamWalker : MonoBehaviour
{
    // BEHOLD, CONVENIENCE
    const string ANIM_ISSLEEPER = "B_isSleeper";
    const string ANIM_ISPATROLLER = "B_isPatroller";
    const string ANIM_JUMP = "b_isJumping";
    const string ANIM_SPOOKED = "t_spooked";
    const string ANIM_FINISHDEBURROW = "b_hasDeburrowed";
    const string ANIM_SEENPLAYER = "b_hasSeenPlayer";
    const string ANIM_DEAD = "b_isDead";

    [Header("Clam Type (Don't Touch)")]
    [Tooltip("Does the clam patrol? Enables below variables to work. You shouldn't touch this.")] [SerializeField]
    protected bool patrols;
    [Tooltip("Is the clam a sleeper clam? Does not function with patrols, and you shouldn't touch it.")] [SerializeField]
    protected bool sleeper;

    [Space(10f)]

    public NavMeshAgent clamNavAgent;
    public ClamScriptObj clamData;
    private Transform playerPos;
    private Vector3 clamJumpTarget;
    private Transform clamTransform;
    private Rigidbody rb;
    private Player_Health playerHealth;
    public Transform looker;
    public GameObject clamBody;
    public ClamAnimPlayer claminatorScript;
    //public Animator animator;

    private bool hasSeenPlayer = false;
    private bool hasDeBurrowed = false;
    private bool isJumping = false;
    private bool isRebounding = false;
    private bool canHurt = false;
    private bool canLook = false;

    [Tooltip("Time it takes for clam to move out of the ground and do first jump. May be replaced with animation events")]
    protected float deBurrowTime;
    private float deBurrowHeight;
    [Tooltip("Cooldown between jumps")]
    protected float jumpCooldown;
    private float curJumpCooldown;
    [Tooltip("The maximum horizontal distance the clam can jump to.")]
    protected float maxJumpDistance = 25;
    [Tooltip("The amount of damage the clam does.")]
    protected int damage;
    
    protected float reboundJumpDelay;
    private float curReboundJumpDelay = 0;

    [Header("Patrol Variables")]
    [Tooltip("'Waypoints' goes here, but you probably shouldn't touch this.")]
    public Transform waypointList;
    private Transform[] waypoints;
    private int waypointIndex = 0;
    private Vector3 waypointTarget;

    // ############## END OF VARIABLES ##############

    private void Start()
    {
        if (sleeper && patrols) {
            Debug.LogWarning("Clam is both a patroller and sleeper!", this); // only one may exist
        }
        rb = GetComponent<Rigidbody>();
        clamTransform = this.GetComponent<Transform>();
        playerHealth = GetComponentInParent<ClamPlayerHealthRef>().GetPlayerHealth(); // this probably isn't doing anything
        playerPos = GetComponentInParent<ClamPlayerHealthRef>().GetPlayer().transform;
        InitBaseDataStats(); // Init variables w/ scriptable object variables
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
        claminatorScript.SetAnimBool(ANIM_ISPATROLLER, patrols);
        claminatorScript.SetAnimBool(ANIM_ISSLEEPER, sleeper);
    }

    private void FixedUpdate()
    {
        if (patrols && !hasSeenPlayer)
        {
            if (Vector3.Distance(clamTransform.position, waypointTarget) < 2f) {
                UpdateClamPatrolDest();
                IterwateWaypointIndex();
            }
        }

        else if (hasDeBurrowed && hasSeenPlayer) // Has clam deburrowed?
        {
            ClamJumpThinklogic();
        }

        else if (hasSeenPlayer && !isRebounding) // Hasn't deburrowed, has it seen the player? If so, run the deburrow timer and look at the player.
        {
            canLook = true;
            if (deBurrowTime > 0) {
                deBurrowTime -= Time.fixedDeltaTime;
            }
            else {
                hasDeBurrowed = true;
                claminatorScript.SetAnimBool(ANIM_FINISHDEBURROW, hasDeBurrowed);
            }
        }
    }

    void Update()
    {
        if (canLook) {
            looker.LookAt(playerPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, looker.rotation, Time.deltaTime * 10);
        } // should probably add a variable for lookspeed but ah well
    }

    // Don't use Update() for AI logic since we don't need to calculate AI stuff every single frame, especially for a mob enemy.
    private void OnTriggerEnter(Collider other) // This is kinda pointless now that detectors exist.
    {
        if (other.CompareTag("Player"))
        {
            Alert(false); // Alert() is public for detector scripts to call
        }
    }

    public void Alert(bool isNoise)
    {
        if (!hasSeenPlayer)
        {
            //if (!isNoise) {
            //    playerPos = other.transform;
            //}
            //else {
            //    playerPos = other.gameObject.GetComponentInParent<Transform>();
            //}
            //Debug.Log(playerPos);

            hasSeenPlayer = true;
            claminatorScript.SetAnimBool(ANIM_SEENPLAYER, hasSeenPlayer);

            if (!sleeper)
            {
                //clamBody.transform.localPosition.Set(0, deBurrowHeight, 0); // 
                clamBody.transform.localPosition = new Vector3(0, deBurrowHeight, 0); // Put clam body above surface if not sleeper
            }

            clamNavAgent.destination = transform.position; // stop patrol when player is detected

            if (patrols) {
                UpdateStats(); // update to alert stats
            }

            if (isNoise) { 
                claminatorScript.SetAnimTrigger(ANIM_SPOOKED); 
            }

            
        }
    }

    private void InitBaseDataStats() // constructors simply break w/ scriptable objects, so it has to be done with a function
    {
        this.deBurrowTime = clamData.deBurrowTime;
        this.deBurrowHeight = clamData.deBurrowHeight;
        this.jumpCooldown = clamData.jumpCooldown;
        this.maxJumpDistance = clamData.maxJumpDistance;
        this.damage = clamData.damage;
        this.reboundJumpDelay = clamData.hitReboundJumpDelay;
    }

    private void UpdateStats()
    {
        clamNavAgent.acceleration = clamData.alertNavAccel;
        clamNavAgent.angularSpeed = clamData.alertNavAngleSpeed;
        clamNavAgent.speed = clamData.alertNavSpeed;
    }

    protected void ClamJumpThinklogic()
    {
        if (curJumpCooldown <= 0 && !isRebounding) { // Is the jump cooldown done? If so, call ClamJump()
            ClamJump();
        }

        else // If jump cooldown isn't done, check velocity to see if clam is done jumping.
        {
            if (clamNavAgent.remainingDistance <= .1f && !isRebounding)
            {
                isJumping = false;
                canHurt = false;
                clamNavAgent.ResetPath();
            }

            if (!isJumping) // If not jumping, check if rebounding
            {
                if (!isRebounding) // If not rebounding, decrement jump cooldown counter and look at the player.
                {
                    curJumpCooldown -= Time.fixedDeltaTime;
                    canLook = true;
                }
                else { // if rebounding, decrement rebound counter and check if it's done
                    curReboundJumpDelay -= Time.fixedDeltaTime; // 50 ms
                    if (curReboundJumpDelay <= 0) {
                        isRebounding = false;
                        clamNavAgent.isStopped = false;
                    }
                }
            }
        }

        claminatorScript.SetAnimBool(ANIM_JUMP, isJumping);
    }

    private void ClamJump()
    {
        clamJumpTarget = Vector3.MoveTowards(clamTransform.position, playerPos.position, maxJumpDistance);
        clamJumpTarget.y = 0;
        clamNavAgent.destination = clamJumpTarget;
        curJumpCooldown = jumpCooldown;
        isJumping = true; // isJumping only exists to make code relating to looking at the player easier to understand
        canHurt = true; // Clam only hurts player when jumping.
        canLook = false; // Don't rotate to face player while jumping
        claminatorScript.SetAnimBool(ANIM_JUMP, isJumping);
    }

    public void AttemptHurt()
    {
        if (canHurt)
        {
            playerHealth.TakeDamage(damage);
            canHurt = false; // Make sure they can't get hurt multiple times in one jump.
            Rebound();
        }
    }

    private void Rebound()
    {
        clamNavAgent.ResetPath(); // CEASE
        clamNavAgent.velocity = Vector3.zero; // THINE
        clamNavAgent.isStopped = true; // M O V E M E N T
        curReboundJumpDelay = reboundJumpDelay;
        rb.AddRelativeForce(Vector3.forward * -clamData.hitReboundPushForce, ForceMode.VelocityChange); // the actual physics, it's kinda borked.
        isJumping = false;
        isRebounding = true;
        canLook = false;
    }

    private void UpdateClamPatrolDest()
    {
        waypointTarget = waypoints[waypointIndex].position;
        clamNavAgent.SetDestination(waypointTarget);
    }

    void IterwateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }

    public void SetDead(bool dead)
    {
        claminatorScript.SetAnimBool(ANIM_DEAD, dead);
        Rebound(); // knockback on death baybee
        enabled = false; // stop logic when ded
    }
}
