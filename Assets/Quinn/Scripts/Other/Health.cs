using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    public int CurrentHP = 1;
    public int MaxHP = 1;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnOverHeal;
    public MyEvent OnZeroHP;

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
}
