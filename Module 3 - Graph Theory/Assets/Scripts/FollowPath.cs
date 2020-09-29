using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    public Color color;
    float speed = 5.0f;
    float accuracy = 2.0f;
    float rotSpeed = 4.0f;
    public GameObject wpManager;
    GameObject wpCopy;
    GameObject[] wps;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;
    public List<UnityAction> buttonCommands = new List<UnityAction>();

    // Start is called before the first frame update
    void Start()
    {
        wpCopy = Instantiate(wpManager);
        wps = wpCopy.GetComponent<WaypointManager>().waypoints;
        graph = wpCopy.GetComponent<WaypointManager>().graph;
        currentNode = wps[0];
        FillButtonList();
    }

    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }
           
        currentNode = graph.getPathPoint(currentWaypointIndex);
 
        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
                            transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction), 
                                                    Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
    //public delegate void ButtonCommands();
    
    void FillButtonList()
    {
        buttonCommands.Add(GoToTwinMountains);
        buttonCommands.Add(Barracks);
        buttonCommands.Add(CommandCenter);
        buttonCommands.Add(OilRefinery);
        buttonCommands.Add(Tankers);
        buttonCommands.Add(Radar);
        buttonCommands.Add(CommandPost);
        buttonCommands.Add(Middle);
    }
    public void GoToTwinMountains()
    {
        graph.AStar(currentNode, wps[4]);
        currentWaypointIndex = 0;
    }
    public void Barracks()
    {
        graph.AStar(currentNode, wps[6]);
        currentWaypointIndex = 0;
    }
    public void CommandCenter()
    {
        graph.AStar(currentNode, wps[9]);
        currentWaypointIndex = 0;
    }
    public void OilRefinery()
    {
        graph.AStar(currentNode, wps[7]);
        currentWaypointIndex = 0;
    }
    public void Tankers()
    {
        graph.AStar(currentNode, wps[16]);
        currentWaypointIndex = 0;
    }
    public void Radar()
    {
        graph.AStar(currentNode, wps[10]);
        currentWaypointIndex = 0;
    }
    public void CommandPost()
    {
        graph.AStar(currentNode, wps[11]);
        currentWaypointIndex = 0;
    }
    public void Middle()
    {
        graph.AStar(currentNode, wps[5]);
        currentWaypointIndex = 0;
    }
}
