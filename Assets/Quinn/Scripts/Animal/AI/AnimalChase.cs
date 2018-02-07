using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChase : AnimalBehavior {
    //what to chase
    public GameObject target;
    //distance to stop chasing the hunter at
    public float StopDistance;
    private float distance;
    //how far infront should it expect the hunter to be
    public float ProjectedDistance;
    public float AttackDistance = 1;
    public int AttackDamage = 1;
    //time it takes to be ready to attack again
    public float AttackSpeed = 1;
    private float AttackSpeedTimer = 0;
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        UpdateDistance();
        UpdateAttackSpeedTimer();
        //is the animal in range and ready to attack?
        if (distance <= AttackDistance && AttackSpeedTimer >= AttackSpeed)
        {
            //deal damage if in range
            target.GetComponent<Health>().IncrementHP(AttackDamage);
        }
        manager.agent.destination = target.transform.position + (target.GetComponent<Rigidbody>().velocity.normalized * ProjectedDistance);
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
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
    private void UpdateAttackSpeedTimer()
    {
        AttackSpeedTimer += Time.deltaTime;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
