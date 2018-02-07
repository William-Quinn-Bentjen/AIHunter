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
        //PickupCheck(other);
    }
    private void PickupCheck(Collider other)
    {
        //Debug.Log("something hit a dead animal");
        //checks to see if a agressive animal or the hunter just collided with it
        if ((other.tag == "Animal" && other.gameObject.GetComponent<AnimalBehaviorManager>().Agressive)|| other.tag == "Hunter")
        {
            if (other.tag == "Hunter")
            {
                //Debug.Log("hunter hit a dead animal");
                //add to inventory
                other.gameObject.GetComponent<HunterInv>().collectRabbitMeat();
            }
            other.gameObject.GetComponent<HunterWander>().enabled = true;
            other.gameObject.GetComponent<HunterChase>().enabled = false;
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
