using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourceManager : MonoBehaviour {
    public bool DestroyAfterPlay = false;
    public bool PlayOnAwake = false;
    private bool played;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if (PlayOnAwake)
        {
            source.playOnAwake = false;
            Play(true);
        }
	}
    //used to play the sound connected to this object
	public void Play(bool restartIfPlaying = false)
    {
        played = true;
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
        if(DestroyAfterPlay)
        {
            if (source.isPlaying == false)
            {
                //sound not playing and it has already played 
                if (played)
                {
                    Destroy(gameObject);
                }
            }
        }
	}
}
