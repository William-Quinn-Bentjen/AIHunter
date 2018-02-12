using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterChase : MonoBehaviour {

    public GameObject chaseTarget;
    NavMeshAgent agent;
    public float distance;
    float gunDistance;
    float viewRange;
    QuinnGun gun;

    HunterWander hunterWander;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject Hunter = GameObject.Find("Hunter");
        hunterWander = Hunter.GetComponent<HunterWander>();
        gun = Hunter.GetComponent<QuinnGun>();
        chaseTarget = hunterWander.target;
        gunDistance = gun.RayCastLength;
        viewRange = hunterWander.length;
    }

    void OnEnable()
    {
        chaseTarget = hunterWander.target;
    }

    // Update is called once per frame
    void Update() {
        agent.destination = chaseTarget.transform.position;
        distance = Vector3.Distance(transform.position, chaseTarget.transform.position);

        if (distance <= gunDistance)
        {
            QuinnGun Fire = gameObject.GetComponent<QuinnGun>();
            gun.Fire();
        }
        else if (distance > viewRange || chaseTarget == null)
        {
            hunterWander.enabled = true;
            hunterWander.target = null;
            this.enabled = false;
            chaseTarget = null;
        }
	}
}
