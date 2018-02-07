using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAgentPather : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public bool editorStop;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.destination = target.transform.position;
        shouldwalk = true;
    }
    bool shouldwalk;
    // Update is called once per frame
    void Update()
    {
        if (editorStop)
        {
            editorStop = false;
            agent.isStopped = !shouldwalk;
            shouldwalk = agent.isStopped;
        }
    }
    public void UpdateDestination(GameObject destination)
    {
        target = destination;
        agent.destination = destination.transform.position;
    }
}