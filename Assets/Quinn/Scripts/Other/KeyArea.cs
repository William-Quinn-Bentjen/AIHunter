using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyArea : MonoBehaviour {
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

    void OnTriggerEnter(Collider other)
    {
        TriggerCheck(other);
    }
    void OnTriggerStay(Collider other)
    {
        TriggerCheck(other);
    }
    private void TriggerCheck(Collider other)
    {
        //did an animal just walk into the area?
        if (other.gameObject.tag == "Animal")
        {
            //is the animal's walkto target this area? 
            AnimalBehaviorManager manager = other.gameObject.GetComponent<AnimalBehaviorManager>();
            if (manager.walkTo.target == gameObject && manager.behaviors.Count > 0)
            {
                //is the animal's current behavior walkto?
                if (manager.behaviors.Peek() == manager.walkTo)
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
            }
        }
    }
}
