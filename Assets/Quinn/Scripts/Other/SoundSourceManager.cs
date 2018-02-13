using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourceManager : MonoBehaviour {
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
    //used to play the sound connected to this object
	public void Play(bool restartIfPlaying = false)
    {
        //should I play the sound if it's already playing
        if (source.isPlaying && restartIfPlaying == false)
        {
            //do nothing the sound is already playing
        }
        else
        {
            //play the sound
            source.Play();
        }
    }
    //used to play the sound connected to another object
    public void PlaySound(string soundName, bool restartIfPlaying = false)
    {
        foreach(GameObject sound in GameObject.FindGameObjectsWithTag("Sound"))
        {
            if (sound.name == soundName)
            {
                sound.GetComponent<SoundSourceManager>().Play(restartIfPlaying);
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
