using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public Camera cam;

    public TaskList task;

    public ResourceManager RM;

    private ActionList AL;

    GameObject targetNode;

    public bool isGathering = false;
    private bool fullYet = false;
    public NavMeshAgent agent;

    public ResourceList heldResourceType;

    public int heldResource;
    public int maxHeldResource;

    public GameObject[] drops;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GatherTick());
        AL = FindObjectOfType<ActionList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("You Left clicked");
            OnLeftClick();
        }

        if (targetNode == null)
        {
            if (isGathering)
            {
                Debug.Log("Resource Depleted");
                isGathering = false;
                if (heldResource != 0)
                {
                    drops = GameObject.FindGameObjectsWithTag("Drops");
                    agent.SetDestination(GetClosestDropOff(drops).transform.position);
                    drops = null;
                    task = TaskList.Delivering;
                }
                else
                {
                    task = TaskList.Idle;
                }
            }
            
            

        }

        if (heldResource >= maxHeldResource)
        {
            //Find and go to drop off point here
            if (fullYet == false)
            {
                Debug.Log("I'm full, heading to drop off");
                fullYet = true;
            }
            drops = GameObject.FindGameObjectsWithTag("Drops");
            agent.SetDestination(GetClosestDropOff(drops).transform.position);
            drops = null;
            task = TaskList.Delivering;
        }
    }

    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
        GameObject closestDrop = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject targetDrop in dropOffs)
        {
            Vector3 direction = targetDrop.transform.position - position;
            float distance = direction.sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDrop = targetDrop;
            }
        }

        return closestDrop;

    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("I hit something");
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            Debug.Log("I'm starting to gather");
            hitObject.GetComponent<NodeManager>().gatherers++;
            heldResourceType = hitObject.GetComponent<NodeManager>().resourceType;
        }
        else if (hitObject.tag == "Drops" && task == TaskList.Delivering)
        {
            //Debug.Log("Working out what resource I have " + heldResourceType);
            //if the worker is carrying stone, drop off the resource and increment the stone counter
            if (heldResourceType == ResourceList.Stone)
            {
                Debug.Log("I have stone, attempting to drop off");
                if (RM.stone >= RM.maxStone)
                {
                    task = TaskList.Idle;
                    Debug.Log("No room for more stone");
                }
                else
                {
                    Debug.Log("Dropped off my stone");
                    RM.stone += heldResource;
                    heldResource = 0;
                    task = TaskList.Gathering;
                    fullYet = false;
                    if (targetNode != null)
                    {
                        agent.SetDestination(targetNode.transform.position);
                    }

                }
            }
            //if the worker is carrying wood, drop off the resource and increment the wood counter
            else if (heldResourceType == ResourceList.Wood)
            {
                Debug.Log("I have wood, attempting to drop off");
                if (RM.wood >= RM.maxWood)
                {
                    task = TaskList.Idle;
                    Debug.Log("No room for more wood");
                }
                else
                {
                    Debug.Log("Dropped off my wood");
                    RM.wood += heldResource;
                    heldResource = 0;
                    task = TaskList.Gathering;
                    fullYet = false;
                    if (targetNode != null)
                    {
                        agent.SetDestination(targetNode.transform.position);
                    }

                }
            }
            //if the worker is carrying wheat, drop off the resource and increment the wheat counter
            else if (heldResourceType == ResourceList.Wheat)
            {
                Debug.Log("I have wheat, attempting to drop off");
                if (RM.wheat >= RM.maxWheat)
                {
                    task = TaskList.Idle;
                    Debug.Log("No room for more wheat");
                }
                else
                {
                    Debug.Log("Dropped off my wheat");
                    RM.wheat += heldResource;
                    heldResource = 0;
                    task = TaskList.Gathering;
                    fullYet = false;
                    if (targetNode != null)
                    {
                        agent.SetDestination(targetNode.transform.position);
                    }

                }
            }
            else
            {
                Debug.Log("I can't work out what resource I have :'(");
            }


        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource")
        {
            isGathering = false;
            hitObject.GetComponent<NodeManager>().gatherers--;
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering == true && task == TaskList.Gathering)
            {
                heldResource++;
            }

        }
    }

    public void OnLeftClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (agent.GetComponent<ClickOn>().currentlySelected == true)
            {
                if (hit.collider.tag == "Ground")
                {
                    AL.Move(agent, hit, task);
                    task = TaskList.Moving;
                }
                else if (hit.collider.tag == "Resource")
                {
                    AL.Harvest(agent, hit, task, targetNode);
                    task = TaskList.Gathering;
                    targetNode = hit.collider.gameObject;
                }
            }
        }
    }
}
