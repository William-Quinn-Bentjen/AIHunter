using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChase : AnimalBehavior {
    //what to chase
    public GameObject target;
    //distance to stop chasing the hunter at
    public float StopDistance;
    private float distance;
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        base.DoBehavior(manager);
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        UpdateDistance();
        if (distance >= StopDistance)
        {
            return true;
        }
        return false;
    }
    public override void UpdateBehavior(AnimalBehaviorManager manager)
    {
        //pop behavior off stack
        if (CheckBehavior(manager))
        {
            manager.behaviors.Pop();
        }
    }
    private void UpdateDistance()
    {
        distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
