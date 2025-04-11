using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script created by Wyatt Blackwell, last edited by ___
 * Modified date: 4/10/2024
 * This script handles clamination by recieving state information from ClamWalker and applying the appropriate animation.
 * In other words, ClamWalker is the brain, this is a leech.
 */

public class ClamAnimPlayer : MonoBehaviour
{
    Animator claminator;
    ClamWalker walker;

    

    private void Start()
    {
        if((claminator = GetComponent<Animator>()) == null) { // Obtain animator component IN the if statement, log error in body
            Debug.LogAssertion("Clam could not find animator component!",this.gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
