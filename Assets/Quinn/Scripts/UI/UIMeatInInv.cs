using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeatInInv : MonoBehaviour {
    private Text output;
    private string initalText;
    private HunterInv hunterInv;
    // Use this for initialization
    void Start () {
        hunterInv = GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterInv>();
        initalText = output.text;
    }
	
	// Update is called once per frame
	void Update () {
        output.text = initalText + hunterInv.rabbitMeat;
	}
}
