using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHunterHP : MonoBehaviour {
    public bool DisplayMaxHP = true;
    private Text output;
    private string initalText;
    private Health hunterHP;
	// Use this for initialization
	void Start () {
        hunterHP = GameObject.FindGameObjectWithTag("Hunter").GetComponent<Health>();
        output = GetComponent<Text>();
        initalText = output.text;
	}
	
	// Update is called once per frame
	void Update () {
        if (DisplayMaxHP)
        {
            output.text = initalText + hunterHP.CurrentHP + " / " + hunterHP.MaxHP;
        }
        else
        {
            output.text = initalText + hunterHP.CurrentHP;
        }
	}
}
