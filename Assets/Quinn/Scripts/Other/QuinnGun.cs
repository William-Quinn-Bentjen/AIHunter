//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class QuinnGun : MonoBehaviour {

//    public int InMag = 5;
//    public int MaxInMag = 5;
//    public int AmmoReserve = 50;
//    public bool InfiniteAmmoReserve = false;
//    public int MaxAmmoReserve = 50;
//    public float ReloadTime = 5;
//    public float FireRPM = 60;
//    public int RayCastDamage = 1; // how much damage to do on hit
//    public float RayCastLength = 10; // how far does the bullet go?
//    [System.Serializable]
//    public class MyEvent : UnityEvent { }
//    public MyEvent OnFire;
//    public MyEvent OnEmptyMag;
//    public MyEvent OnDryFire;
//    public MyEvent OnReloadStart;
//    public MyEvent OnReloadComplete;
//    //private
//    private float ReadyToFireTime; //chambered at 0 (used for RPM control)
//    private bool Reloading; //reloading ammo?
//    private float ReloadProgress; // seconds since the reload stared

//    // public functions (USE THESE FOR AI AND PLAYER INTERACTION)
//    // used to see if there is a bullet in the chamber ready to fire
//    public bool ChamberedCheck()
//    {
//        //gun mag in, has bullets and the gun has chambered a shot 
//        if (Reloading == false && InMag > 0 && ReadyToFireTime >= 0)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//    //pulls the "trigger" (will not fire without a round in the chamber and a mag in)
//    public void Fire()
//    {
//        if (ChamberedCheck())
//        {
//            //fire
//            //raycast
//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, transform.forward, out hit, RayCastLength))
//            {
//                if (hit.collider.tag == "Animal")
//                {
//                    //deal damage to whatever we hit
//                    hit.collider.GetComponent<Health>().TakeDamage(RayCastDamage);
//                }
//            }

//            //fire
//            InMag--;
//            ReadyToFireTime = -1 / (FireRPM / 60);
//            OnFire.Invoke();
//            if (InMag <= 0)
//            {
//                OnEmptyMag.Invoke();
//            }

//        }
//        else if (Reloading == false && InMag <= 0)
//        {
//            OnDryFire.Invoke();
//        }
//    }
//    //starts the reload process and calls OnReloadStart's events
//    public void ReloadStart()
//    {
//        if (Reloading != true && AmmoReserve > 0)
//        {
//            OnReloadStart.Invoke();
//            ReloadProgress = 0;
//            Reloading = true;
//        }
//    }
//    //called to keep track of RPM and pace the shots
//    private void ReadyToFireUpdate()
//    {
//        if (ChamberedCheck() == false)
//        {
//            ReadyToFireTime += Time.deltaTime;
//        }
//    }
//    //called to keep track of time for realod 
//    private void ReloadUpdate()
//    {
//        if (Reloading == true)
//        {
//            ReloadProgress += Time.deltaTime;
//            if (ReloadProgress >= ReloadTime)
//            {
//                Reload();
//            }
//        }
//    }
//    //reloads the gun and calls OnReloadComplete's events
//    //NOTE USE RELOADSTART TO RELOAD AS Reload() will reload the gun instantly
//    private void Reload()
//    {
//        ReadyToFireTime = 0;
//        Reloading = false;
//        if (!InfiniteAmmoReserve)
//        {
//            //reload the ammo for non infinite guns
//            if (AmmoReserve < MaxInMag)
//            {
//                InMag = AmmoReserve;
//                AmmoReserve = 0;
//            }
//            else
//            {
//                InMag = MaxInMag;
//                AmmoReserve -= MaxInMag;
//            }
//        }
//        else
//        {
//            //reload ammo for infinite ammo gun
//            InMag = MaxInMag;
//        }
//        OnReloadComplete.Invoke();
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        ReloadUpdate();
//        ReadyToFireUpdate();
//    }
//}
