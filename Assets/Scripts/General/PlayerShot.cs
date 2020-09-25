using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : Projectile
{
    [SerializeField] private int xDir;
    [SerializeField] private int yDir;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("dead", dead);
        transform.position = new Vector3(transform.position.x + (speed * xDir * Time.fixedDeltaTime), transform.position.y + (speed * yDir * Time.fixedDeltaTime), transform.position.z);
        if (transform.position.x > BOUNDS || transform.position.x < -BOUNDS || transform.position.y < 0 || transform.position.y > YBOUND)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void setDirs(int xdir, int ydir)
    {
        xDir = xdir;
        yDir = ydir;
    }

    public override void reflect()
    {
        dir = -dir;
        target = -target;
    }
}
