using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour {
    public AudioSource music;
    private Toggle checkBox;
	// Use this for initialization
	void Start () {
        checkBox = GetComponent<Toggle>();
        //music = GetComponent<AudioSource>();
        music.mute = checkBox.isOn;
	}
	public void ToggleMute()
    {
        checkBox.isOn = !checkBox.isOn;
        music.mute = !music.mute;
    }
	// Update is called once per frame
	void Update () {
        //check for hotkey and update checkbox which should call this script to change the music
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMute();
            }
        }
    }
}
