using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public Transform player;
    float speed;
    UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        this.agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        speed = agent.speed;
    }
    void Update() 
    {
        agent.SetDestination(player.position);

        if(Vector3.Distance(this.transform.position, player.position) < 8.0f)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = speed;
        }
    }
}
