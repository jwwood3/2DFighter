﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : Entity
{
    [SerializeField] private bool parrying;
    [SerializeField] private bool shooting;
    [SerializeField] private bool canParry;
    [SerializeField] private bool canShoot;
    [SerializeField] private bool shouldParry;
    [SerializeField] private float iFrames;
    [SerializeField] private float invulnerabilityCounter;
    [SerializeField] private bool canSwitch;
    [SerializeField] private PlayerShot bullet;
    [SerializeField] private bool[] abilities;
    [SerializeField] private float xBulletOffset;
    [SerializeField] private float yBulletOffset;
    [SerializeField] private int[] dirs;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
        abilities = new bool[] { true, false };// Shoot, Shoot neutral
        dirs = new int[] { 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            setFacingUp();
            if (canDoMove())
            {
                if (canParry && Input.GetButtonDown("Parry"))
                {
                    parrying = true;
                    canParry = false;
                }
                else if (abilities[0] && canShoot && Input.GetButton("Shoot"))
                {
                    shooting = true;
                    canShoot = false;
                }
            }
            if (Input.GetButtonDown("Jump") && grounded)
            {
                jumping = true;
                jumpTime = 0.0f;
            }
            if (Input.GetButtonUp("Jump") && jumping)
            {
                jumping = false;
                fallSpeed = 0.0f;
            }
        }
        base.Update();
        anim.SetBool("parrying", parrying);
        anim.SetBool("shooting", shooting);
        anim.SetBool("canSwitch", canSwitch);
    }

    void FixedUpdate()
    {
        if (alive)
        {
            if (invulnerable)
            {
                invulnerabilityCounter -= Time.fixedDeltaTime;
                if (invulnerabilityCounter <= 0)
                {
                    invulnerabilityCounter = 0;
                    invulnerable = false;
                }
            }
            if (Input.GetButton("MoveRight"))
            {
                faceDir = true;
                moving = true;
                moveLeftRight(Time.fixedDeltaTime);
            }
            else if (Input.GetButton("MoveLeft"))
            {
                faceDir = false;
                moving = true;
                moveLeftRight(Time.fixedDeltaTime);
            }
            else
            {
                moving = false;
            }
        }
        else
        {
            moving = false;
            parrying = false;
            shouldParry = false;
            invulnerable = false;
            jumping = false;
        }
        base.FixedUpdate();
    }

    public override bool isTargeted(int t)
    {
        return t == 1;
    }

    public void endParry(int faceRight)
    {
        parrying = false;
        canSwitch = true;
        faceDir = faceRight==1;
    }

    protected override void gravStep(float time)
    {
        fallSpeed += fallAccel * time;
        float newY;
        if (this.transform.position.y - fallSpeed > GROUND_LEVEL)
        {
            newY = this.transform.position.y - fallSpeed;
        }
        else
        {
            newY = GROUND_LEVEL;
            canParry = true;
            canShoot = true;
            fallSpeed = 0.0f;
        }
        this.transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);
    }

    void shoot()
    {
        int xdir = dirs[0];
        int ydir = dirs[1];
        if(xdir==0 && ydir == 0 && !abilities[1])
        {
            xdir = faceDir ? 1 : -1;
        }
        PlayerShot newBullet = Instantiate(bullet, new Vector3(this.transform.position.x + (xdir * xBulletOffset), this.transform.position.y + (ydir * yBulletOffset), this.transform.position.z), Quaternion.identity);
        newBullet.setDirs(xdir, ydir);
    }

    void endShoot(int faceRight)
    {
        shooting = false;
        faceDir = faceRight == 1;
        canSwitch = true;
    }

    void startParryWindow()
    {
        shouldParry = true;
    }

    void endParryWindow()
    {
        shouldParry = false;
    }

    public bool willParry()
    {
        return shouldParry;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "damage")
        {
            col.gameObject.SendMessage("dealDamage", (Entity)this.gameObject.GetComponent<PlayerCon>());
        }
    }

    public override void takeDamage(int dmg)
    {
        base.takeDamage(dmg);
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerabilityCounter = iFrames;
        }
        if (Health <= 0)
        {
            Health = 0;
            alive = false;
        }
    }

    public override void Reset()
    {
        base.Reset();
        parrying = false;
        canParry = true;
        shouldParry = false;
        invulnerabilityCounter = 0;
        canSwitch = true;
        this.transform.position = new Vector3(0.8f * LEFT_BOUND, GROUND_LEVEL, 0.0f);
        Update();
    }

    public void endSwitchWindow()
    {
        canSwitch = false;
    }

    bool canDoMove()
    {
        return !parrying && !shooting;
    }

    void setFacingUp()
    {
        if (Input.GetButton("MoveUp"))
        {
            faceUp = 1;
        }
        else if (Input.GetButton("MoveDown"))
        {
            faceUp = -1;
        }
        else
        {
            faceUp = 0;
        }
    }

    int pressingVert()
    {
        if (Input.GetButton("MoveUp"))
        {
            return 1;
        }
        else if (Input.GetButton("MoveDown"))
        {
            return -1;
        }
        return 0;
    }

    int pressingHoriz()
    {
        if (Input.GetButton("MoveRight"))
        {
            return 1;
        }
        else if (Input.GetButton("MoveLeft"))
        {
            return -1;
        }
        return 0;
    }

    void storeDirs()
    {
        dirs[0] = pressingHoriz();
        dirs[1] = pressingVert();
    }
}
