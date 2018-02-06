using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterPursuit : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    Vector3 Hunter;
    Vector3 fwd;
    public float length = 5.0f;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Hunter = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
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
         
        int numOfRays = 15;
        fwd = transform.forward;
        fwd = Quaternion.AngleAxis(-45, Vector3.up) * fwd;
        for(int i = 0; i < numOfRays; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Hunter, fwd * length, out hit))
            {
                if(hit.collider.gameObject.tag == "Animal")
                {
                    // Add Behavior when target seen
                    Debug.Log("Target Detected");
                }
               
            }
            Debug.DrawRay(Hunter, fwd * length, Color.green);
            fwd = Quaternion.AngleAxis(90 / numOfRays, Vector3.up) * fwd;
        }
	}
}
