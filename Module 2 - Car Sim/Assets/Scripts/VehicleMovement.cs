using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Transform goal; 
    public float speed = 0;
    public float rotSpeed = 15;
    public float acceleration = 5;
    public float deceleration = 5;
    public float minSpeed = 0;
    public float maxSpeed = 30;
    public float breakAngle = 20;

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, goal.position.y, goal.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction), 
                                                    Time.deltaTime * rotSpeed);

        if (Vector3.Angle(goal.forward, this.transform.forward) < breakAngle && speed > 10)
        {
            speed = Mathf.Clamp(speed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
        }

        //Not adding Time.deltaTime causes the car to move extremely fast, I think Unity changed something and I have to do this
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
