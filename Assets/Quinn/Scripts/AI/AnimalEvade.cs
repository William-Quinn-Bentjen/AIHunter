using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEvade : AnimalBehavior
{
    public GameObject target;
    //distance to stop evading at 
    public float SafeDistance = 10;
    //distance evade starts at
    public float SpookDistance = 3;

    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        manager.agent.destination = transform.position + ((target.transform.position - gameObject.transform.position).normalized * -SafeDistance);
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        //distance between animal and hunter
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //can I stop running now?
        if (distance >= SafeDistance)
        {
            Debug.Log("I'm not run");
            return true;
        }
        Debug.Log("I'm also not run");
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
