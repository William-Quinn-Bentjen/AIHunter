﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterWander : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    Vector3 Hunter;
    Vector3 fwd;
    public float length = 20.0f;

    public float wanderRadius;
    public float wanderTimer;

    Transform Target;
    float timer;

    

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Hunter = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        timer = wanderTimer;
        target = null;
	}
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }


    void Update () {
        //RaycastHit hit;
        //fwd = transform.TransformDirection(Vector3.forward);
        //// Ray Vision = new Ray(Hunter, fwd * length);
        //Debug.DrawRay(transform.position, fwd * length, Color.green);

        //if (Physics.Raycast(transform.position, fwd, out hit, length))
        //{

        //    if (hit.collider.gameObject.tag == "Animal")
        //    {
        //        Debug.Log("HIT");
        //    }
        //}
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        int numOfRays = 15;
        fwd = transform.forward;
        fwd = Quaternion.AngleAxis(-45, Vector3.up) * fwd;
        for(int i = 0; i < numOfRays; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd * length, out hit, length))
            {
                if(hit.collider.gameObject.tag == "Animal")
                {
                    // Add Behavior when target seen
                    target = hit.collider.gameObject;
                    GetComponent<HunterChase>().enabled = true;
                    GetComponent<HunterWander>().enabled = false;
                    Debug.Log("Target Detected");
                }
               
            }
            Debug.DrawRay(transform.position, fwd * length, Color.green);
            fwd = Quaternion.AngleAxis(90 / numOfRays, Vector3.up) * fwd;
        }

    }
}
