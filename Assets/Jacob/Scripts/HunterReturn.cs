using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterReturn : MonoBehaviour {

    public GameObject homeBase;
    NavMeshAgent agent;
    QuinnGun ammo;
    HunterInv meat;
    Health health;
    public float hunterSpeed;
    public int runHealth = 5;
    float speed;
    int maxHealth;
    int currentHealth;
    int totalAmmo;
    int totalMeat;
    int ammoMax;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        GameObject homeBase = GameObject.Find("HomeBase");
        GameObject Hunter = GameObject.Find("Hunter");
        agent.speed = hunterSpeed;
        speed = hunterSpeed;
        health = Hunter.GetComponent<Health>();
        ammo = Hunter.GetComponent<QuinnGun>();
        meat = Hunter.GetComponent<HunterInv>();
        maxHealth = health.MaxHP;
        currentHealth = health.CurrentHP;
        totalAmmo = ammo.AmmoReserve;
        ammoMax = ammo.MaxAmmoReserve;
        totalMeat = meat.rabbitMeatDrop;
	}
	
	// Update is called once per frame
	void Update () {
        if (totalAmmo <= 0 || totalMeat >= 5)
        {
            GetComponent<HunterChase>().enabled = false;
            GetComponent<HunterWander>().enabled = false;
            agent.destination = homeBase.transform.position;
        }

        if (totalAmmo == ammoMax && currentHealth == maxHealth)
        {
            speed = hunterSpeed;
            GetComponent<HunterWander>().enabled = true;
            
        }

        if (currentHealth <= runHealth)
        {
            GetComponent<HunterWander>().enabled = false;
            agent.destination = homeBase.transform.position;
            agent.speed = agent.speed * 2;
        }
	}
}
