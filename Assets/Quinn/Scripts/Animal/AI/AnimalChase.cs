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
    private bool killedTarget = false;
    private bool killedAnimal = false;
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        GameObject test = target;
        if (test != null)
        {
            //if (target == null)
            //{
            //    Debug.Log("NULL BITCH");
            //}
            //update distance to target and timer on how offten the animal can attack
            UpdateDistance();
            UpdateAttackSpeedTimer();
            //is the animal in range and ready to attack?
            if (distance <= AttackDistance && AttackSpeedTimer >= AttackSpeed)
            {
                //deal damage if in range
                //Debug.Log("AGGRESSIVE ANIMAL ATTACKED " + target.name + " FOR " + AttackDamage + " DAMAGE\nHUNTER HP NOW " + target.GetComponent<Health>().CurrentHP);
                //did I just kill the thing I was chasing?
                if (target.GetComponent<Health>().CurrentHP - AttackDamage <= 0)
                {
                    killedTarget = true;
                    if (target.tag == "Animal")
                    {
                        killedAnimal = true;
                    }
                }
                target.GetComponent<Health>().IncrementHP(-AttackDamage);
            }
            manager.agent.destination = target.transform.position + (target.GetComponent<Rigidbody>().velocity.normalized * ProjectedDistance);
            Debug.DrawLine(transform.position, manager.agent.destination, Color.red);
        }
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        GameObject test = target;
        //is the object past the stop distance and not in our vision?
        if (((distance >= StopDistance && !manager.vision.visibleTargets.Contains(target.transform)) || killedTarget == true) && test != null)
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
            manager.GetComponent<FieldOfView>().FindVisibleTargets();
        }
    }
    private void UpdateDistance()
    {
        //used to avoid errors
        if (target == null)
        {
            distance = StopDistance;
        }
        else
        {
            distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        }
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
