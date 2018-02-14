using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class HunterCamp : MonoBehaviour {
    public bool VisibleAtStart = false;
    public int Meat = 0;
    [Header("HUNTER IS IN CAMP ADD AMOUNT AND START COOLDOWN TIMER IF TIMER UP ADD AGAIN")]
    public float Cooldown = 1;
    public int HealAmount = 5;
    public int AmmoAmount = 5;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnMeatDrop;
    public MyEvent OnHeal;
    public MyEvent OnAmmoRefill;
    //private
    private Renderer rend;
    private float TimeAdded = 0;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hunter")
        {
            Health hunterHP = other.gameObject.GetComponent<Health>();
            QuinnGun hunterWeapon = other.gameObject.GetComponent<QuinnGun>();
            HunterInv hunterInventory = other.gameObject.GetComponent<HunterInv>();
            //is the cooldown time good?
            if (TimeAdded + Cooldown - Time.time < 0)
            {
                TimeAdded = Time.time;
                //drop off meat
                if (hunterInventory.rabbitMeat > 0)
                {
                    Meat += hunterInventory.rabbitMeat;
                    hunterInventory.rabbitDropOff();
                    OnMeatDrop.Invoke();
                }
                //heal hunter
                if (hunterHP.CurrentHP != hunterHP.MaxHP)
                {
                    hunterHP.IncrementHP(HealAmount);
                    OnHeal.Invoke();
                }

                //add ammo to hunter's inventory
                if (hunterWeapon.AmmoReserve < hunterWeapon.MaxAmmoReserve)
                {
                    hunterWeapon.AmmoReserve += AmmoAmount;
                    if (hunterWeapon.AmmoReserve >= hunterWeapon.MaxAmmoReserve)
                    {
                        hunterWeapon.AmmoReserve = hunterWeapon.MaxAmmoReserve;
                    }
                    OnAmmoRefill.Invoke();
                }
                //is the hunter's mag empty? if so call reload (this is for if the hunter enters the camp with no ammo so the gun will reload if it's empty)
                if (hunterWeapon.InMag <= 0)
                {
                    hunterWeapon.ReloadStart();
                }
            }
            //if hunter at max hp and ammo in reserve and has no meat in their inventory then have the hunter wander again
            if (hunterHP.CurrentHP == hunterHP.MaxHP && hunterWeapon.AmmoReserve == hunterWeapon.MaxAmmoReserve && hunterInventory.rabbitMeat == 0)
            {
                other.gameObject.GetComponent<NavMeshAgent>().speed = other.gameObject.GetComponent<HunterReturn>().hunterSpeed;
                other.gameObject.GetComponent<HunterWander>().enabled = true;
            }
        }
    }
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        if (VisibleAtStart)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
