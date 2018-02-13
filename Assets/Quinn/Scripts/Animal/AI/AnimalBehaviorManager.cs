using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimalBehaviorManager : MonoBehaviour
{
    [Header("REQUIRES A KEYAREA, KEYAREAHOLDER AND HUNTER TO WORK")]
    [HideInInspector]
    public Stack<AnimalBehavior> behaviors;
    [HideInInspector]
    public NavMeshAgent agent;
    //used to tell the animal to go back to where it spawned from
    public GameObject Den;
    private GameObject hunter;
    public float SpookDistance = 3;
    public bool Agressive = false;
    public float ChaseDistance = 4;
    private KeyAreas keyAreas;
    [Header("Spawn behavior generation settings")]
    public int minWander=1;
    public int maxWander=1;
    public int minWalkTo=1;
    public int maxWalkTo=1;
    [HideInInspector]
    //animal vision
    public FieldOfView vision;
    //behaviors
    [HideInInspector]
    public AnimalEvade evade;
    [HideInInspector]
    public AnimalWalkTowards walkTo;
    [HideInInspector]
    public AnimalWander wander;
    [HideInInspector]
    //only used if aggressive 
    public AnimalChase chase;
    [Header("READ ONLY")]
    public AnimalBehavior CurrentBehavior;
    private int wanderTotal;
    private int walkToTotal;
    [HideInInspector]
    public Stack<GameObject> walkToTargets = new Stack<GameObject>();



    // Use this for initialization
    void Start()
    {
        keyAreas = GameObject.FindGameObjectWithTag("KeyAreaHolder").GetComponent<KeyAreas>();
        vision = GetComponent<FieldOfView>();
        evade = GetComponent<AnimalEvade>();
        walkTo = GetComponent<AnimalWalkTowards>();
        wander = GetComponent<AnimalWander>();
        chase = GetComponent<AnimalChase>();
        agent = GetComponent<NavMeshAgent>();
        behaviors = new Stack<AnimalBehavior>();
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        //CONSTRUCTION ZONE
        //ADD BEHAVIORS
        wanderTotal = Random.Range(minWander, maxWander);
        walkToTotal = Random.Range(minWalkTo, maxWalkTo);
        //set walk to targets
        for (int i = 0; i < walkToTotal; i++)
        {
            walkToTargets.Push(keyAreas.GetRandomArea(true, false, false));
        }
        //add behaviors to stack in random order
        int wanderTotalCounter = 0;
        int walkToTotalCounter = 0;
        for (int i = 0; i < wanderTotal + walkToTotal; i++)
        {
            //add wander
            if (wanderTotalCounter < wanderTotal)
            {
                wanderTotalCounter++;
                behaviors.Push(wander);
            }
            //add walk to
            else if (walkToTotalCounter < walkToTotal)
            {
                walkToTotalCounter++;
                behaviors.Push(walkTo);
            }
        }
        //END CONSTRUCTION ZONE
        //old spawn behaviors
        //behaviors.Push(evade);
        walkTo.target = keyAreas.GetRandomArea(true,false,false);
        behaviors.Push(walkTo);
        behaviors.Push(wander);
    }
    void DoBehavior()
    {
        behaviors.Peek().DoBehavior(this);
        behaviors.Peek().UpdateBehavior(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (behaviors.Count > 0)
        {
            CurrentBehavior = behaviors.Peek();
            var currentBehavior = behaviors.Peek();
            //is the animal currently chasing or evading?
            if (currentBehavior == chase || currentBehavior == evade )
            {
                //fight or flight mode, animal wont add new behaviors until the current finishes
            }
            else
            {
                //is the animal looking for something to chase or run from?
                if (Agressive)
                {
                    //CHECK IF HUNTER IS CLOSE ENOUGH TO CAUSE CHASE
                    if (Vector3.Distance(hunter.transform.position, agent.gameObject.transform.position) <= SpookDistance && currentBehavior != chase)
                    {
                        chase.target = hunter;
                        behaviors.Push(chase);
                    }
                    else
                    {
                        //CHECK IF THE ANIMAL CAN SEE THE HUNTER, DEAD ANIMAL OR ANIMAL AND CHASE IF ANIMAL SPOT SPOT THEM
                        if(vision.visibleTargets.Count > 0)
                        {
                            if (vision.visibleTargets.Contains(hunter.transform))
                            {
                                chase.target = hunter;
                                behaviors.Push(chase);
                            }
                            else
                            {
                                //MAKE SURE WHAT I WAS LOOKING AT DIDN'T DISAPPEAR (stops null errors after an animal dies)
                                Transform destroyedTest = vision.visibleTargets[0].transform;
                                if (destroyedTest != null)
                                {
                                    //look for dead things if none are avalible hunt the closest animal
                                    float smallestDistance = Vector3.Distance(gameObject.transform.position, vision.visibleTargets[0].position);
                                    bool foundDeadAnimal = false;
                                    GameObject agressiontarget = vision.visibleTargets[0].gameObject;
                                    foreach (Transform target in vision.visibleTargets)
                                    {
                                        if (target != null)
                                        {

                                            if (target.gameObject.tag == "DeadAnimal")
                                            {
                                                foundDeadAnimal = true;
                                                if (smallestDistance >= Mathf.Min(smallestDistance, Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position)))
                                                {
                                                    smallestDistance = Mathf.Min(smallestDistance, Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position));
                                                    agressiontarget = target.gameObject;
                                                }


                                            }
                                            if (foundDeadAnimal == false && target.gameObject.tag == "Animal")
                                            {
                                                if (smallestDistance >= Mathf.Min(smallestDistance, Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position)))
                                                {
                                                    smallestDistance = Mathf.Min(smallestDistance, Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position));
                                                    agressiontarget = target.gameObject;
                                                }
                                            }
                                            
                                        }
                                    }
                                    if (foundDeadAnimal)
                                    {
                                        //sets walkto target to the closest animal if no dead animals or hunters were found
                                        walkTo.target = agressiontarget;
                                        behaviors.Push(walkTo);
                                    }
                                    else
                                    {
                                        //sets chase target to the closest animal if no dead animals or hunters were found
                                        chase.target = agressiontarget;
                                        behaviors.Push(chase);
                                    }
                                }
                                
                            }
                        }
                    }
                }
                else
                {
                    //CHECK IF THE HUNTER IS CLOSE ENOUGH TO SPOOK THE ANIMAL
                    if (Vector3.Distance(hunter.transform.position, agent.gameObject.transform.position) <= SpookDistance && currentBehavior != evade)
                    {
                        //Debug.Log("ANIMAL SPOOKED");
                        evade.target = hunter;
                        behaviors.Push(evade);
                    }
                    else
                    {
                        //CHECK IF THE ANIMAL CAN SEE THE HUNTER OR AGRESSIVE ANIMAL AND RUN IF ANIMAL SPOT THEM
                        //TO BE ADDED
                        //IF THEY CANT SEE ANYONE DO WHATEVER THEY WERE DOING
                        
                    }
                }
            }
        }
        else
        {
            //Debug.Log("animal has no behaviors, returning to den");
            walkTo.target = Den;
            behaviors.Push(walkTo);
        }
        //does the behavior
        //Debug.Log("HUNTER " + hunter.gameObject.transform.position);
        DoBehavior();
        ////is the animal spooked, chasing, or should it continue what it was doing?
        //if (behaviors.Count > 0)
        //{
        //    currentBehavior = behaviors.Peek();
        //}
        //else
        //{
        //    //currentbehavior is just used to see if the behavior is evade or not check below so no errors happen if the count is empty
        //    currentBehavior = null;
        //}
        //if (currentBehavior != chase || currentBehavior != evade)
        //{
        //    if (Agressive)
        //    {
        //        //CHECK IF THE HUNTER IS CLOSE ENOUGH TO CHASE
        //        if (Vector3.Distance(hunter.transform.position, agent.gameObject.transform.position) <= ChaseDistance)
        //        {
        //            //Debug.Log("ANIMAL NOW CHASEING HUNTER");
        //            chase.target = hunter;
        //            behaviors.Push(chase);
        //        }
        //    }
        //    else
        //    {
        //        //CHECK IF THE HUNTER IS CLOSE ENOUGH TO SPOOK THE ANIMAL
        //        if (Vector3.Distance(hunter.transform.position, agent.gameObject.transform.position) <= SpookDistance && currentBehavior != evade)
        //        {
        //            //Debug.Log("ANIMAL SPOOKED");
        //            evade.target = hunter;
        //            behaviors.Push(evade);
        //        }
        //    }
        //}



        //else
        //{
        //    if (behaviors.Count > 0)
        //    {
        //        behaviors.Peek().DoBehavior(this);
        //        behaviors.Peek().UpdateBehavior(this);
        //    }
        //    else
        //    {
        //        Debug.Log("animal has no behaviors, returning to den");
        //        walkTo.target = Den;
        //        behaviors.Push(walkTo);
        //    }
        //}
    }
}