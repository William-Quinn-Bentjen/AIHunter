using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoInMag : MonoBehaviour {
    public bool DisplayMaxInMag = true;
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
        if (DisplayMaxInMag)
        {
            output.text = initalText + gun.InMag + " / " + gun.MaxInMag;
        }
        else
        {
            output.text = initalText + gun.InMag;
        }
    }
}
