using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnimalPickup : MonoBehaviour {
    public bool VisibleAtStart = false;
    private GameObject hunter;
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
                hunter.GetComponent<HunterInv>().collectRabbitMeat();
                hunter.GetComponent<HunterWander>().enabled = true;
                hunter.GetComponent<HunterChase>().enabled = false;
                hunter.GetComponent<HunterChase>().chaseTarget = null;
                hunter.GetComponent<HunterWander>().target = null;
            }
            //was hunter walking to this object? if so tell him to wander instead
            if (hunter.GetComponent<HunterChase>().enabled == true && hunter.GetComponent<HunterChase>().chaseTarget == gameObject)
            {
                hunter.GetComponent<HunterWander>().enabled = true;
                hunter.GetComponent<HunterChase>().enabled = false;
                hunter.GetComponent<HunterChase>().chaseTarget = null;
                hunter.GetComponent<HunterWander>().target = null;
            }
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        if (!VisibleAtStart)
        {
            GetComponent<Renderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
