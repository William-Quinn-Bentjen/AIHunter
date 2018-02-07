using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimalPickup : MonoBehaviour {
    public bool VisibleAtStart = false;
    private void OnTriggerEnter(Collider other)
    {
        PickupCheck(other);
    }
    private void OnTriggerStay(Collider other)
    {
        PickupCheck(other);
    }
    private void PickupCheck(Collider other)
    {
        if (other.tag == "Animal" || other.tag == "Hunter")
        {
            if (other.tag == "Hunter")
            {
                //add to inventory
            }
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        if (!VisibleAtStart)
        {
            GetComponent<Renderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
