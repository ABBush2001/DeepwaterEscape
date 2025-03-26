using UnityEngine;

[CreateAssetMenu(fileName = "ClamData", menuName = "ScriptableObjects/ClamScriptableObject", order = 1)]
public class ClamScriptObj : ScriptableObject
{
    [Header("Basic Stats")]
    [Tooltip("Time it takes for clam to move out of the ground and do first jump. May be replaced with animation events")]
    public float deBurrowTime = 0.6f;
    [Tooltip("Cooldown between jumps")]
    public float jumpCooldown = 1.5f;
    [Tooltip("The maximum horizontal distance the clam can jump to.")]
    public float maxJumpDistance = 25f;
    [Tooltip("The amount of damage the clam does.")]
    public int damage = 20;

    [Header("Ambush Nav Values (Nerd Stuff)")]
    public float navSpeed;
    public float navAngleSpeed;
    public float navAccel;

    [Header("Patrol Nav Values (Nerd Stuff)")]
    [Tooltip("Speed of clam when calm")]
    public float patrolNavSpeed = 0;
    [Tooltip("Rotation speed of clam when calm")]
    public float patrolNavAngleSpeed = 0;
    [Tooltip("Acceleration speed of clam when calm")]
    public float patrolNavAccel = 0;

    [Header("Patrol Alert Nav Values (Nerd Stuff)")]
    [Tooltip("Speed of clam when angry")]
    public float patrolAlertNavSpeed = 0;
    [Tooltip("Rotation speed of clam when angry")]
    public float patrolAlertNavAngleSpeed = 0;
    [Tooltip("Acceleration speed of clam when angry")]
    public float patrolAlertNavAccel = 0;
}