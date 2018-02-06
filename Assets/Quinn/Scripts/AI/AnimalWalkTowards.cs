using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWalkTowards : AnimalBehavior
{

    public GameObject target;
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        //logic
        manager.agent.destination = target.transform.position;
        Debug.DrawLine(gameObject.transform.position, manager.agent.destination, Color.blue); 
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        //Condition
        return PathComplete(manager);
    }
    public override void UpdateBehavior(AnimalBehaviorManager manager)
    {
        //if my path is complete pop the behavior 
        if (PathComplete(manager))
        {
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
