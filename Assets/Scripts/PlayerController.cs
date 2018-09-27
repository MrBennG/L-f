using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedThing = hit.collider.gameObject;
                print(clickedThing.layer);
                if (clickedThing.layer != 9)
                {
                    if (agent.GetComponent<ClickOn>().currentlySelected == true)
                    {
                        agent.SetDestination(hit.point);
                    }
                }
                
            }
            
        }
    }
}
