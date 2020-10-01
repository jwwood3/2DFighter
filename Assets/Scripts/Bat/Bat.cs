using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField] private float dashChance = 5.0f;
    [SerializeField] private float xBulletOffset = 2.0f;
    [SerializeField] private float yBulletOffset = 2.0f;
    [SerializeField] private bool shooting = false;
    [SerializeField] private bool dashing = false;
    [SerializeField] private Bat_Spike bullet;
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
        anim.SetBool("shooting", shooting);
    }

    void FixedUpdate()
    {
        float dec = dashChance / 100.0f;
        float prob = (float)(1.0f - Mathf.Log(1.0f - dec, 120));
        if (gen is null)
        {
            gen = new System.Random();
        }
        base.Update();
        anim.SetInteger("delaying", delaying);
        anim.SetBool("shooting", shooting);
        if (alive)
        {
            if (delaying > 0)
            {
                return;
            }
            if (!shooting && !dashing)
            {
                int num = gen.Next(1, 1001);
                if (faceDir != ((player.transform.position.x - transform.position.x) > 0))
                {
                    delaying = 2;
                    faceDir = ((player.transform.position.x - transform.position.x) > 0);
                    return;
                }
                faceDir = (player.transform.position.x - transform.position.x) > 0;
                if (num < (1000.0f * prob))
                {
                    shooting = true;
                    return;
                }

                if (num > ((1000.0f - (1000.0f * prob)) / 2.0f + (1000.0f * prob)))
                {
                    //dashing = true;
                    return;
                }

            }
        }
    }

    void stopShooting()
    {
        shooting = false;
        delaying = 1;

    }

    void stopdashing()
    {
        dashing = false;
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
