using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ClamAmbushScriptableObject", order = 1)]
public class ClamScriptableObjAmbush : ScriptableObject
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

    
}