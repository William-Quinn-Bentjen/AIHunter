﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalWander : AnimalBehavior {
    [Header("Wander settings")]
    public float raduis = 3;
    public float jitter = 1;
    public float distance = 5;
    [Header("How long to wander for")]
    //min and max time to wander in seconds
    public float minWanderTime = 1;
    public float maxWanderTime = 600;
    [Header("READ ONLY")]
    public Vector3 target;
    //private
    float wanderTime = 0;
    float targetWanderTime;

    // Use this for initialization
    void Start () {
        targetWanderTime = Random.Range(minWanderTime, maxWanderTime);
	}
    public override void UpdateBehavior(AnimalBehaviorManager manager)
    {
        if (CheckBehavior(manager))
        {
            manager.behaviors.Pop();
            targetWanderTime = Random.Range(minWanderTime, maxWanderTime);
            wanderTime = 0;
        }
    }
    public override bool CheckBehavior(AnimalBehaviorManager manager)
    {
        wanderTime += Time.deltaTime;
        //has the animal wandered long enough?
        if (wanderTime >= targetWanderTime)
        {
            return true;
        }
        return false;
    }
    public override void DoBehavior(AnimalBehaviorManager manager)
    {
        target = Vector3.zero;
        target = Random.insideUnitCircle.normalized * raduis;
        target = (Vector2)target + Random.insideUnitCircle * jitter;

        target.z = target.y;
        target += transform.position;
        target += transform.forward * distance;
        target.y = transform.position.y;
        manager.agent.destination = target;
        Debug.DrawLine(gameObject.transform.position, manager.agent.destination, Color.green);
    }
}
