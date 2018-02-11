using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour {
    [Header("Den settings")]
    public GameObject Animal;
    [Header("Time in between spawns")]
    public float minSpawnTimer = 10;
    public float maxSpawnTimer = 60;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    [Header("NOTE if hunter is targeting animal it will automatically return to wander")]
    public MyEvent OnReturnToDen;
    //private
    private GameObject DespawnZone;
    private float nextSpawnTime;
    private float spawnTimer;

    public void SpawnAnimal()
    {
        DespawnZone = gameObject;
        spawnTimer = 0;
        nextSpawnTime = Random.Range(minSpawnTimer, maxSpawnTimer);
        GameObject spawnedAnimal = Instantiate(Animal, transform.position, transform.rotation);
        spawnedAnimal.GetComponent<AnimalBehaviorManager>().Den = gameObject;
        Debug.Log("spawned animal at " + spawnedAnimal.GetComponent<AnimalBehaviorManager>().Den);
        //Debug.Log("spawned an animal at \n" + transform.position);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnAnimal();
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        //did an animal just walk into the area?
        if (other.gameObject.tag == "Animal")
        {
            //is the animal's walkto target this gameobject's position? 
            AnimalBehaviorManager manager = other.gameObject.GetComponent<AnimalBehaviorManager>();
            if (manager.walkTo.target == gameObject && manager.behaviors.Count > 0)
            {
                //is the animal's current behavior walkto?
                if (manager.behaviors.Peek() == manager.walkTo)
                {
                    //if the hunter was targeting the animal have him wander again
                    GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
                    if (hunter.GetComponent<HunterChase>().chaseTarget == other.gameObject)
                    {
                        hunter.GetComponent<HunterWander>().enabled = true;
                        hunter.GetComponent<HunterChase>().enabled = false;
                    }
                    //animal arrived, destroy animal 
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
