using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Enemy
{
    [SerializeField] private float bulletChance = 5.0f;
    [SerializeField] private float xBulletOffset = 2.0f;
    [SerializeField] private float yBulletOffset = 2.0f;
    [SerializeField] private bool shooting = false;
    [SerializeField] private bool swiping = false;
    [SerializeField] private float SWIPE_RANGE;
    [SerializeField] private WaterBall bullet;
    [SerializeField] private Damage swipe;
    private System.Random gen;
    // Start is called before the first frame update
    protected override void Start()
    {
        gen = new System.Random();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        anim.SetInteger("delaying", delaying);
    }

    void FixedUpdate()
    {
        float dec = bulletChance / 100.0f;
        float prob = (float)(1.0f - Mathf.Log(1.0f - dec, 120));
        if(gen is null)
        {
            gen = new System.Random();
        }
        base.Update();
        anim.SetInteger("delaying", delaying);
        anim.SetBool("shooting", shooting);
        anim.SetBool("swiping", swiping);
        if (alive)
        {
            if (delaying > 0)
            {
                return;
            }
            if(!shooting && !swiping)
            {
                int num = gen.Next(1, 1001);
                if(faceDir!=((player.transform.position.x - transform.position.x) > 0))
                {
                    delaying = 2;
                    moving = false;
                    faceDir = ((player.transform.position.x - transform.position.x) > 0);
                    return;
                }
                faceDir = (player.transform.position.x - transform.position.x) > 0;
                if (System.Math.Abs(player.transform.position.x - transform.position.x) < SWIPE_RANGE && num<(1000.0f*prob))
                {
                    swiping = true;
                    moving = false;
                    return;
                }
                
                if (num < 1000.0f*prob)
                {
                    moving = false;
                    shooting = true;
                    return;
                }

                /*if (num < 8)
                {
                    moving = true;
                }*/
                
            }
            if (moving)
            {
                moveLeftRight(Time.fixedDeltaTime);
            }
            
            
        }

    }

    void activateSwipe()
    {
        swipe.activate();
    }

    void stopShooting()
    {
        shooting = false;
        moving = false;
        delaying = 1;

    }

    void stopSwiping()
    {
        swiping = false;
        moving = false;
        delaying = 1;
    }

    void shoot()
    {
        int dir = 1;
        if (faceDir == false) dir = -1;
        WaterBall newBullet = Instantiate(bullet, new Vector3(this.transform.position.x + (dir * xBulletOffset), this.transform.position.y + (yBulletOffset), this.transform.position.z),Quaternion.identity);
        newBullet.setDirs(dir, 0);
    }

    public override void Reset()
    {
        base.Reset();
        shooting = false;
        swiping = false;
    }

    

}
