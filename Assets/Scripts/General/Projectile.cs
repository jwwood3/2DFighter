using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Damage
{
    [SerializeField] protected int xDir;
    [SerializeField] protected int yDir;
    [SerializeField] protected float speed;
    protected float currentTime;
    [SerializeField] protected float maxTime;
    [SerializeField] protected float BOUNDS;
    [SerializeField] protected float YBOUND;
    [SerializeField] protected float FLOORBOUND;
    [SerializeField] protected bool dead;
    [SerializeField] protected Animator anim;

    protected virtual void Start()
    {
        currentTime = 0.0f;
    }

    protected virtual void Update()
    {
        anim.SetInteger("xDir", xDir);
        anim.SetInteger("yDir", yDir);
        anim.SetBool("dead", dead);

        if (this.transform.position.x>BOUNDS || this.transform.position.x<-BOUNDS || this.transform.position.y>YBOUND || this.transform.position.y < FLOORBOUND)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x + (speed * xDir * Time.fixedDeltaTime), transform.position.y + (speed * yDir * Time.fixedDeltaTime), transform.position.z);
        if (currentTime >= maxTime)
        {
            dead = true;
        }
    }

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
        xDir = -xDir;
        yDir = -yDir;
        target = -target;
    }

    public void setDirs(int xdir, int ydir)
    {
        xDir = xdir;
        yDir = ydir;
    }
}
