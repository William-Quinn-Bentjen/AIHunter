using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour {
    public GameObject Animal;
    [Header("Time in between spawns")]
    public float minSpawnTimer = 10;
    public float maxSpawnTimer = 60;
    //private 
    private float nextSpawnTime;
    private float spawnTimer;

    public void SpawnAnimal()
    {
        spawnTimer = 0;
        nextSpawnTime = Random.Range(minSpawnTimer, maxSpawnTimer);
        GameObject spawnedAnimal = Instantiate(Animal, transform.position, transform.rotation);
        spawnedAnimal.GetComponent<AnimalBehaviorManager>().Den = gameObject;
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
}
