using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterInv : MonoBehaviour {

    public int rabbitMeat;


	// Use this for initialization
	void Start () {
        rabbitMeat = 0;
	}
	
	public void collectRabbitMeat()
    {
        rabbitMeat += 1;
    }
}
