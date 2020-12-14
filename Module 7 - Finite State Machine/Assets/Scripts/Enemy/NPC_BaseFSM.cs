using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BaseFSM : StateMachineBehaviour
{
    public GameObject npc;
    public GameObject target;
    public GameObject[] waypoints;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.gameObject;
        waypoints = npc.GetComponent<TankAI>().waypoints;
        target = npc.GetComponent<TankAI>().GetPlayer();
    }
}
