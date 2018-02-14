using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEvade : AnimalBehavior
{
    [Header("When does the animal think it's safe?")]
    //distance to stop evading at 
    public float SafeDistance = 10;
    //what to run from 
    [Header("READ ONLY")]
    public GameObject target;

    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        //sets destination based on player position and target position (target is what it is running from)
        manager.agent.destination = transform.position + ((target.transform.position - gameObject.transform.position).normalized * -SafeDistance);
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        //distance between animal and hunter
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //is the animal a "safe" distace away?
        if (distance >= SafeDistance)
        {
            return true;
        }
        return false;
    }
    public override void UpdateBehavior(AnimalBehaviorManager manager)
    {
        Debug.DrawLine(gameObject.transform.position, manager.agent.destination, Color.red);
        //if the animal is far enough away from the hunter stop evading
        if (CheckBehavior(manager))
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
