using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimalBehaviorManager : MonoBehaviour
{
    [Header("REQUIRES A KEYAREA, KEYAREAHOLDER AND HUNTER TO WORK")]
    [HideInInspector]
    public Stack<AnimalBehavior> behaviors;
    AnimalBehavior currentBehaviour;
    [HideInInspector]
    public NavMeshAgent agent;
    //used to tell the animal to go back to where it spawned from
    public GameObject Den;
    private GameObject hunter;
    public float SpookDist = 3;
    public bool Agressive = false;
    private KeyAreas keyAreas;
    //behaviors
    [HideInInspector]
    public AnimalEvade evade;
    [HideInInspector]
    public AnimalWalkTowards walkTo;
    [HideInInspector]
    public AnimalWander wander;



    // Use this for initialization
    void Start()
    {
        keyAreas = GameObject.FindGameObjectWithTag("KeyAreaHolder").GetComponent<KeyAreas>();
        evade = GetComponent<AnimalEvade>();
        walkTo = GetComponent<AnimalWalkTowards>();
        wander = GetComponent<AnimalWander>();
        agent = GetComponent<NavMeshAgent>();
        behaviors = new Stack<AnimalBehavior>();
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        //behaviors.Push(evade);
        walkTo.target = keyAreas.GetRandomArea();
        behaviors.Push(walkTo);
        behaviors.Push(wander);
    }

    // Update is called once per frame
    void Update()
    {
        //is the animal spooked? or should it continue what it was doing.
        if (behaviors.Count > 0)
        {
            currentBehaviour = behaviors.Peek();
        }
        else
        {
            //just used for spook check below so no errors happen if the count is empty
            currentBehaviour = wander;
        }
        
        if (Vector3.Distance(hunter.transform.position, agent.gameObject.transform.position) <= SpookDist && currentBehaviour != evade)
        {
            //Debug.Log("ANIMAL SPOOKED");
            evade.target = hunter;
            behaviors.Push(evade);
        }
        else
        {
            if (behaviors.Count > 0)
            {
                behaviors.Peek().DoBehavior(this);
                behaviors.Peek().UpdateBehavior(this);
            }
            else
            {
                Debug.Log("animal has no behaviors, returning to den");
                walkTo.target = Den;
                behaviors.Push(walkTo);
            }
        }
    }
}