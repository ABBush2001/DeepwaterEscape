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

    private void Start()
    {
        clamTransform = this.GetComponent<Transform>();
        playerHealth = GetComponentInParent<ClamPlayerHealthRef>().GetPlayerHealth();
    }

    private void FixedUpdate()
    {
        if (hasDeBurrowed) // Has clam deburrowed?
        {
            if (curJumpCooldown <= 0) { // Is the jump cooldown done? If so, call ClamJump()
                ClamJump(); 
            }

            else // If jump cooldown isn't done, check velocity to see if clam is done jumping.
            {
                //if (clamNavAgent.velocity.sqrMagnitude <= 0.1f)
                if (clamNavAgent.remainingDistance <= 0.1f)
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

        else if (hasSeenPlayer) // Hasn't deburrowed, has it seen the player? If so, run the deburrow timer and look at the player.
        { 
            clamTransform.LookAt(playerPos);
            deBurrowTime -= Time.fixedDeltaTime;

            if (deBurrowTime <= 0) { 
                hasDeBurrowed = true; 
            }
        }
    }

    // Don't use Update() since we don't need to calculate AI stuff every single frame, especially for a mob enemy.
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (!hasSeenPlayer) {
                clamNavAgent.baseOffset += 5; // Offset is just until animations get in
            }
            hasSeenPlayer = true;
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
            canHurt = false; // Make sure they can't get hurt multiple times in one jump.
            //hitStop = true;
            //Time.timeScale = 0f;
        }
    }

}
