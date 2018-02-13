using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCamp : MonoBehaviour {
    public int Meat = 0;
    [Header("EVERY FRAME HUNTER IS IN CAMP ADD")]
    public int HealAmount = 5;
    public int AmmoAmount = 5;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hunter")
        {
            Health hunterHP = other.gameObject.GetComponent<Health>();
            QuinnGun hunterWeapon = other.gameObject.GetComponent<QuinnGun>();
            HunterInv hunterInventory = other.gameObject.GetComponent<HunterInv>();
            //drop off meat
            Meat += hunterInventory.rabbitMeat;
            hunterInventory.rabbitDropOff();
            //heal hunter
            hunterHP.IncrementHP(HealAmount);
            //add ammo to hunter's inventory
            if(hunterWeapon.AmmoReserve + AmmoAmount <= hunterWeapon.MaxAmmoReserve)
            {
                hunterWeapon.AmmoReserve += AmmoAmount;
            }
            else
            {
                hunterWeapon.AmmoReserve = hunterWeapon.MaxAmmoReserve;
            }
            //is the hunter's mag empty? if so call reload (this is for if the hunter enters the camp with no ammo so the gun will reload if it's empty)
            if (hunterWeapon.InMag <= 0)
            {
                hunterWeapon.ReloadStart();
            }
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
