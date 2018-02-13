using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Health : MonoBehaviour {
    public int CurrentHP = 1;
    public int MaxHP = 1;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnOverHeal;
    public MyEvent OnZeroHP;
    public GameObject DeadAnimal;

    // Use this for initialization
    void Start () {
        CurrentHP = MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
	}

    //used to deal damage (calls increment with a negitive value)
    public void TakeDamage(int value)
    {
        IncrementHP(-value);
    }

    //increments the value of current HP
    public void IncrementHP(int value)
    {
        SetHP(CurrentHP + value);
    }

    //sets the value of the HP bar
    public void SetHP(int value)
    {
        CurrentHP = value;
        if (CurrentHP <= 0)
        {
                OnZeroHP.Invoke();
        }
        else if (CurrentHP > MaxHP)
        {
            OnOverHeal.Invoke();
        }
    }

    //used to disable the animal's script, as well as mark it as dead & ready to be picked up
    public void KillAnimal()
    {
        Debug.Log("ANIMAL KILLED AT\n"+gameObject.transform.position);
        //stores transform info so the dead animal doesn't spawn with it's live self inside it
        Transform DeathPos = gameObject.transform;
        //destroy live animal 
        Destroy(gameObject);
        //create dead animal prefab where the animal was at
        Instantiate(DeadAnimal, DeathPos.position, DeathPos.rotation);
    }
}
