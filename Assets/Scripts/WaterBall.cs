using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Projectile
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void dealDamage(Entity guy)
    {
        base.dealDamage(guy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("dead", dead);
        transform.position = new Vector3(transform.position.x + (speed * dir * Time.fixedDeltaTime), transform.position.y, transform.position.z);
        if(transform.position.x>BOUNDS || transform.position.x < -BOUNDS)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public override void reflect()
    {
        dir = -dir;
        target = -target;
    }
}
