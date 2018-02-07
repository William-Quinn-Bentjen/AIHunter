using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour {

    public virtual void DoBehavior(AnimalBehaviorManager manager)
    {

    }
    public virtual bool CheckBehavior(AnimalBehaviorManager manager)
    {
        return false;
    }
    public virtual void UpdateBehavior(AnimalBehaviorManager manager)
    {

    }
    protected bool PathComplete(AnimalBehaviorManager manager)
    {
        if (!manager.agent.pathPending)
        {
            if (manager.agent.stoppingDistance >= manager.agent.remainingDistance)
            {
                if (!manager.agent.hasPath || manager.agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
