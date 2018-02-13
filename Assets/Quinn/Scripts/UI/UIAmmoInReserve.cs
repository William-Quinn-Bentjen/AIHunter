using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoInReserve : MonoBehaviour
{
    public bool DisplayMaxInReserve = true;
    private Text output;
    private string initalText;
    private QuinnGun gun;
    // Use this for initialization
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Hunter").GetComponent<QuinnGun>();
        output = GetComponent<Text>();
        initalText = output.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayMaxInReserve)
        {
            output.text = initalText + gun.AmmoReserve + " / " + gun.MaxAmmoReserve;
        }
        else
        {
            output.text = initalText + gun.AmmoReserve;
        }
    }
}