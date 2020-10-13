using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    protected NavMeshAgent agent;
    public GameObject target;
    public WASDMovement playerMovement;
    [SerializeField] protected float sightDistance = 20;
    protected Vector3 wanderTarget;
    protected void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }
    protected void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }
    protected void Flee(Vector3 location)
    {
        Vector3 fleeDireciton = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDireciton);
    }
    protected void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude/(agent.speed + playerMovement.currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }
    protected void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude/(agent.speed + playerMovement.currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    protected void Wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                    0,
                                    Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }
    protected void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if(spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }
        Seek(chosenSpot);
    }
    protected void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if(spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }
        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 100f;
        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * 5);
    }

    protected bool InRange()
    {
        Vector3 targetPos = target.transform.position - this.transform.position;
        return Vector3.Distance(targetPos, this.transform.position) < sightDistance;
    }
    protected bool CanSeeTarget() 
    {
        RaycastHit rayCastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if(Physics.Raycast(this.transform.position, rayToTarget, out rayCastInfo));
        {
            return rayCastInfo.transform.gameObject.tag == "Player";
        }
        return false;
    }
}
