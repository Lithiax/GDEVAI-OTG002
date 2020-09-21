using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    int currentWaypointIndex = 0;
    float speed = 20;
    float rotSpeed = 3;
    float accuracy = 1;

    void Start()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0)
            return;

        GameObject currentWayPoint = circuit.Waypoints[currentWaypointIndex].gameObject;
        Vector3 LookAtGoal = new Vector3(currentWayPoint.transform.position.x, 
                                            this.transform.position.y, 
                                            currentWayPoint.transform.position.z);

        Vector3 direction = LookAtGoal - this.transform.position;

        if(direction.magnitude < accuracy)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= circuit.Waypoints.Length)
                currentWaypointIndex = 0;
        }

        this.transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                    Quaternion.LookRotation(direction), 
                                                    Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
