using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damage
{
    [SerializeField] protected int dir;
    [SerializeField] protected float speed;
    [SerializeField] protected float BOUNDS;
    [SerializeField] protected float YBOUND;
    [SerializeField] protected bool dead;
    [SerializeField] protected Animator anim;

    protected virtual void endDie()
    {
        Destroy(gameObject);

    }

    public override void dealDamage(Entity guy)
    {
        if (dead)
        {
            return;
        }
        if (guy.isTargeted(target) && active)
        {
            guy.takeDamage(power);
            dead = true;
        }
    }

    public virtual void setDead()
    {
        dead = true;
    }

    public virtual void reflect()
    {
        
    }

    public virtual void setDir(int newDir)
    {
        dir = newDir;
    }
}
