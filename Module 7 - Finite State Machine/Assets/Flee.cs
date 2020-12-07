using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NPC_BaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 fleeDireciton = animator.transform.position - target.transform.position;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(fleeDireciton), 1.0f * Time.deltaTime);
        npc.transform.Translate(0, 0, Time.deltaTime * 2.0f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
