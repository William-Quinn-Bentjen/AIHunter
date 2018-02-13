using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HunterCamp : MonoBehaviour {
    public bool VisibleAtStart = false;
    public int Meat = 0;
    [Header("EVERY FRAME HUNTER IS IN CAMP ADD")]
    public int HealAmount = 5;
    public int AmmoAmount = 5;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnMeatDrop;
    public MyEvent OnHeal;
    public MyEvent OnAmmoRefill;
    //private
    private Renderer rend;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hunter")
        {
            Health hunterHP = other.gameObject.GetComponent<Health>();
            QuinnGun hunterWeapon = other.gameObject.GetComponent<QuinnGun>();
            HunterInv hunterInventory = other.gameObject.GetComponent<HunterInv>();
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
            if(hunterWeapon.AmmoReserve + AmmoAmount < hunterWeapon.MaxAmmoReserve)
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
