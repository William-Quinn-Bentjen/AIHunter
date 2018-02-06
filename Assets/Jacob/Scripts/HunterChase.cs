using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterChase: MonoBehaviour {

    public GameObject chaseTarget;
    NavMeshAgent agent;

	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject Hunter = GameObject.Find("Hunter");
        HunterWander hunterWander = Hunter.GetComponent<HunterWander>();
        chaseTarget = hunterWander.target;
    }
	
	// Update is called once per frame
	void Update () {
        agent.destination = chaseTarget.transform.position;
	}
}
