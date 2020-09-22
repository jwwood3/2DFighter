using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] protected int target;
    [SerializeField] protected int power;
    [SerializeField] protected bool piercing;
    [SerializeField] protected bool active=true;

    public virtual void dealDamage(Entity guy)
    {
        if (guy.isTargeted(target) && active)
        {
            guy.takeDamage(power);
        }
    }

    public void activate()
    {
        active = true;
    }

    public void deactivate()
    {
        active = false;
    }

    public bool isActive()
    {
        return active;
    }

    public bool isPiercing()
    {
        return piercing;
    }

    public int getTarget()
    {
        return target;
    }

    public int getPower()
    {
        return power;
    }
}
