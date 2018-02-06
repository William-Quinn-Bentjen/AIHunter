using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAreas : MonoBehaviour {
    //list of areas 
    [Header("USED FOR DEBUGGING DON'T ADD AREAS BELOW")]
    public List<GameObject> Areas = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        RefreshKeyAreas();
    }
    //returns a random area
    public GameObject GetRandomArea()
    {
        if (Areas.Count > 0)
        {
            return Areas[Random.Range(0, Areas.Count)];
        }
        else
        {
            Debug.Log("no areas in key areas, passing this gameobject");
            return gameObject;
        }
    }
    //returns a list of all game areas
    public List<GameObject> GetAllAreas()
    {
        return Areas;
    }
    //refreshes key areas
    public void RefreshKeyAreas()
    {
        Areas = new List<GameObject>();
        foreach (GameObject area in GameObject.FindGameObjectsWithTag("KeyArea"))
        {
            Areas.Add(area);
        }
        
    }
}
