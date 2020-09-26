using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : Projectile
{
    [SerializeField] private int xDir;
    [SerializeField] private int yDir;
    [SerializeField] private float maxTime;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        anim.SetBool("dead", dead);
        transform.position = new Vector3(transform.position.x + (speed * xDir * Time.fixedDeltaTime), transform.position.y + (speed * yDir * Time.fixedDeltaTime), transform.position.z);
        if (currentTime >= maxTime)
        {
            dead = true;
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
