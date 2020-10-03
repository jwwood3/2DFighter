using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField] private float dashChance = 20;
    [SerializeField] private float shootChance = 70;
    [SerializeField] private float xBulletOffset = 2.0f;
    [SerializeField] private float yBulletOffset = 2.0f;
    [SerializeField] private bool shooting = false;
    [SerializeField] private bool dashing = false;
    [SerializeField] private int dashPhase;
    [SerializeField] private float flyHeight;
    [SerializeField] private Bat_Spike bullet;
    [SerializeField] private Vector3 ground_target;
    [SerializeField] private Damage bodyBox;
    private System.Random gen;
    // Start is called before the first frame update
    protected override void Start()
    {
        gen = new System.Random();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        anim.SetInteger("delaying", delaying);
        anim.SetBool("shooting", shooting);
        Debug.Log("pos: " + transform.position);
    }

    protected override void FixedUpdate()
    {
        if (gen is null)
        {
            gen = new System.Random();
        }
        base.Update();
        if (alive)
        {
            if (delaying > 0)
            {
                return;
            }
            if (!shooting && !dashing)
            {
                activateHitbox();
                int num = gen.Next(1, 101);
                if (num < shootChance)
                {
                    shooting = true;
                    return;
                }
                else if (num < shootChance+dashChance)
                {
                    dashing = true;
                    dashPhase = 1;
                    target = GameMaster.getRandomPosition(flyHeight, GROUND_LEVEL, H_BOUND, -H_BOUND);
                    ground_target = GameMaster.getGroundPosition(this, target);
                    return;
                }
                else
                {
                    delaying = 3;
                }
            }
            if (dashing)
            {
                if(dashPhase == 1)
                {
                    moveTowards(ground_target, Time.fixedDeltaTime);
                    if (isAt(ground_target))
                    {
                        dashPhase = 2;
                    }
                }
                else if(dashPhase == 2)
                {
                    moveTowards(target, Time.fixedDeltaTime);
                    if (isAt(target))
                    {
                        dashPhase = 0;
                        dashing = false;
                        delaying = 3;
                    }
                }
            }
        }
    }

    void activateHitbox()
    {
        bodyBox.activate();
    }

    void stopShooting()
    {
        shooting = false;
        delaying = 1;

    }

    void shoot(int num)
    {
        if (num == 1)
        {
            Bat_Spike newBullet2 = Instantiate(bullet, new Vector3(this.transform.position.x + (xBulletOffset), this.transform.position.y, this.transform.position.z), Quaternion.identity);
            newBullet2.setDirs(1, 0);
            Bat_Spike newBullet4 = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet4.setDirs(0, 1);
            Bat_Spike newBullet5 = Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y - (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet5.setDirs(0, -1);
            Bat_Spike newBullet7 = Instantiate(bullet, new Vector3(this.transform.position.x - (xBulletOffset), this.transform.position.y, this.transform.position.z), Quaternion.identity);
            newBullet7.setDirs(-1, 0);

        }
        else
        {
            Bat_Spike newBullet1 = Instantiate(bullet, new Vector3(this.transform.position.x + (xBulletOffset), this.transform.position.y + (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet1.setDirs(1, 1);
            Bat_Spike newBullet3 = Instantiate(bullet, new Vector3(this.transform.position.x + (xBulletOffset), this.transform.position.y - (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet3.setDirs(1, -1);
            Bat_Spike newBullet6 = Instantiate(bullet, new Vector3(this.transform.position.x - (xBulletOffset), this.transform.position.y + (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet6.setDirs(-1, 1);
            Bat_Spike newBullet8 = Instantiate(bullet, new Vector3(this.transform.position.x - (xBulletOffset), this.transform.position.y - (yBulletOffset), this.transform.position.z), Quaternion.identity);
            newBullet8.setDirs(-1, -1);
        }
    }

    public override void Reset()
    {
        base.Reset();
        shooting = false;
        dashing = false;
    }
}
