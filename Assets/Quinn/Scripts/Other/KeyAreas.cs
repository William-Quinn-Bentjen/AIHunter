using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAreas : MonoBehaviour {
    //list of areas 
    [Header("USED FOR DEBUGGING DON'T ADD AREAS BELOW")]
    public List<GameObject> TaggedKeyAreas = new List<GameObject>();
    public List<GameObject> Dens = new List<GameObject>();
    public List<GameObject> HunterCamps = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        RefreshTaggedKeyAreas();
    }

    //returns a random area
    public GameObject GetRandomArea(bool includeTaggedKeyAreas = true, bool includeDens = true, bool includeHunterCamps = true)
    {
        if (TaggedKeyAreas.Count > 0)
        {
            return TaggedKeyAreas[Random.Range(0, TaggedKeyAreas.Count)];
        }
        else
        {
            Debug.Log("no areas in key areas, passing this GameObject to avoid an error");
            return gameObject;
        }
    }
    //returns a list of all game areas
    public List<GameObject> GetAllAreas()
    {
        return TaggedKeyAreas;
    }
    //refreshes key areas
    public void RefreshTaggedKeyAreas()
    {
        foreach (GameObject area in GameObject.FindGameObjectsWithTag("KeyArea"))
        {
            TaggedKeyAreas.Add(area);
        }
        foreach (GameObject area in GameObject.FindGameObjectsWithTag("Den"))
        {
            Dens.Add(area);
        }
        foreach (GameObject area in GameObject.FindGameObjectsWithTag("HunterCamp"))
        {
            Dens.Add(area);
        }
    }
}
