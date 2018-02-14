using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidArea : MonoBehaviour {
    public bool VisibleAtStart = false;
    private Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        if (!VisibleAtStart)
        {
            rend.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void TriggerCheck(Collider other)
    {
        //did an animal just walk into the area?
        if (other.gameObject.tag == "Animal")
        {
            //is the animal's walkto target this area? 
            AnimalBehaviorManager manager = other.gameObject.GetComponent<AnimalBehaviorManager>();
            if (manager.behaviors.Count > 0)
            {
                if (manager.behaviors.Peek() == manager.evade)
                {
                    //do nothing already evading
                }
                else if (manager.behaviors.Peek() == manager.walkTo && manager.walkTo.target == gameObject)
                {
                    //animal arrived and behavior poped
                    manager.behaviors.Pop();
                    //do I need to pop a walk to target too?
                    if (manager.walkToTargets.Count > 0)
                    {
                        if (manager.walkToTargets.Peek() == gameObject)
                        {
                            manager.walkToTargets.Pop();
                        }
                    }
                }
                //if they were chasing something run from it instead
                else if (manager.behaviors.Peek() == manager.chase && manager.chase.target != null)
                {
                    manager.evade.target = manager.chase.target;
                    manager.behaviors.Pop();
                    manager.behaviors.Push(manager.evade);
                }
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        TriggerCheck(other);
    }
}
