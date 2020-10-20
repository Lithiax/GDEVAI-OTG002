using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator animator;
    float speedMult;
    float detectionRadius = 20;
    float flockRadius = 10;
    
    void ResetAgent()
    {
        speedMult = Random.Range(0.5f, 1.5f);
        agent.speed = 2 * speedMult;
        agent.angularSpeed = 120;
        animator.SetFloat("speedMultiplier", speedMult);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        animator.SetFloat("wOffset", Random.Range(0.1f, 1f));
        agent.ResetPath();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < 2)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectFlockObstacle(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 flockDirection = (location - this.transform.position).normalized;
            Vector3 newGoal = this.transform.position + flockDirection * flockRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(location);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }

        }
    }
    public void DetectFleeObstacle(Vector3 location)
    {
        if (Vector3.Distance(this.transform.position, location) < detectionRadius)
        {
            Vector3 flockDirection = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + flockDirection * flockRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }

        }
    }
}
