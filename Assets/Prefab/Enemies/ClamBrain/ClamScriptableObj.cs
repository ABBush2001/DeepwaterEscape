using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ClamScriptableObject", order = 1)]
public class ClamScriptableObj : ScriptableObject
{
    [Header("Ambush Values")]
    [Tooltip("The maximum horizontal distance the clam can jump to.")]
    public float ambushMaxJumpDistance;
    [Tooltip("The amount of damage the clam does.")]
    public int ambushDamage;
    [Tooltip("Time it takes for clam to move out of the ground and do first jump. May be replaced with animation events")]
    public float ambushDeBurrowTime;
    [Tooltip("Cooldown between jumps")]
    public float ambushJumpCooldown;

    [Header("Ambush Nav Values")]
    public float ambushNavSpeed;
    public float ambushNavAngleSpeed;
    public float ambushNavAccel;

    [Header("Patrol Values")]
    [Tooltip("The maximum horizontal distance the clam can jump to.")]
    public float patrolMaxJumpDistance;
    [Tooltip("The amount of damage the clam does.")]
    public int patrolDamage;
    [Tooltip("Time it takes for clam to move out of the ground and do first jump. May be replaced with animation events")]
    public float patrolDeBurrowTime;
    [Tooltip("Cooldown between jumps")]
    public float patrolJumpCooldown;

    [Header("Patrol Nav Values")]
    [Tooltip("Speed of clam")]
    public float patrolNavSpeed;
    [Tooltip("Rotation speed of clam")]
    public float patrolNavAngleSpeed;
    public float patrolNavAccel;

    [Header("Patrol Alert Nav Values")]
    public float patrolAlertNavSpeed;
    public float patrolAlertNavAngleSpeed;
    public float patrolAlertNavAccel;
}
