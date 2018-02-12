using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWalkTowards : AnimalBehavior
{
    [Header("READ ONLY")]
    public GameObject target;
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        //logic
        if (target != null)
        {
            manager.agent.destination = target.transform.position;
        }
        else
        {
            manager.agent.destination = gameObject.transform.position;
        }
        Debug.DrawLine(gameObject.transform.position, manager.agent.destination, Color.blue); 
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        //Condition
        if (target != null)
        {
            return PathComplete(manager);
        }
        return true;
        
    }
    public override void UpdateBehavior(AnimalBehaviorManager manager)
    {
        //if my path is complete pop the behavior 
        if (PathComplete(manager))
        {
            //did i just reach the den?
            if (target == manager.Den)
            {
                //was the hunter chasing me?
                GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
                if (hunter.GetComponent<HunterWander>().target == gameObject)
                {
                    //tell the hunter to start wandering again
                    hunter.GetComponent<HunterWander>().enabled = true;
                    hunter.GetComponent<HunterChase>().enabled = false;
                }
                //reached den, despawning
                Debug.Log("animal reached den and despawned");
                Destroy(gameObject);
            }
            manager.behaviors.Pop();
        }
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
