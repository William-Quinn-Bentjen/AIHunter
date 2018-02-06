using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimalBehaviorManager : MonoBehaviour
{
    [HideInInspector]
    public Stack<AnimalBehavior> behaviors;
    [HideInInspector]
    public NavMeshAgent agent;
    [Header("example")]
    public GameObject thingThatHoldsBehaviors;
    //behaviors
    private AnimalEvade evade;
    private AnimalWalkTowards walkTo;
    // Use this for initialization
    void Start()
    {
        evade = GetComponent<AnimalEvade>();
        walkTo = GetComponent<AnimalWalkTowards>();
        agent = GetComponent<NavMeshAgent>();
        behaviors = new Stack<AnimalBehavior>();
        behaviors.Push(evade);
        behaviors.Push(walkTo);
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviors.Count > 0)
        {
            behaviors.Peek().DoBehavior(this);
            behaviors.Peek().UpdateBehavior(this);
        }
        else
        {
            Debug.Log("I have no behaviors");
            //wander?
        }
    }
}