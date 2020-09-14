using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public Transform goal;
    float rotSpeed = 3;
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        Vector3 direction = lookAtGoal - transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);


        if (Vector3.Distance(lookAtGoal, transform.position) > 2)
        {
            transform.position = Vector3.Lerp(transform.position, goal.position, Time.deltaTime);
        }
    }
}
