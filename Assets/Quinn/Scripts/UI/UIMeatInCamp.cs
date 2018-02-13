﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMeatInCamp : MonoBehaviour {

    private Text output;
    private string initalText;
    private HunterCamp camp;
    // Use this for initialization
    void Start()
    {
        camp = GameObject.FindGameObjectWithTag("KeyAreaHolder").GetComponent<KeyAreas>().HunterCamps[0].GetComponent<HunterCamp>();
        initalText = output.text;
    }

    // Update is called once per frame
    void Update()
    {
        output.text = initalText; //+ hunterInv.rabbitMeat;
    }
}
