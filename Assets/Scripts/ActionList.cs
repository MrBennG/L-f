using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionList : MonoBehaviour
{
    public void Move(NavMeshAgent agent, RaycastHit hit, TaskList task)
    {
        agent.SetDestination(hit.point);
        Debug.Log("I'm moving!");
    }

    public void Harvest(NavMeshAgent agent, RaycastHit hit, TaskList task, GameObject targetNode)
    {
        agent.SetDestination(hit.collider.transform.position);
        Debug.Log("Off to harvest!");
    }
}
