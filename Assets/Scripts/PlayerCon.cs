using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCon : Entity
{
    [SerializeField] private bool parrying;
    [SerializeField] private bool canParry;
    [SerializeField] private bool shouldParry;
    [SerializeField] private float iFrames;
    [SerializeField] private float invulnerabilityCounter;
    [SerializeField] public int parryMode;
    [SerializeField] private bool canSwitch;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
    }

    bool isPressingJump()
    {
        Keyboard curKeyb = Keyboard.current;
        Gamepad curGamep = Gamepad.current;
        bool keyPressing = (curKeyb != null) && (curKeyb.spaceKey.isPressed || curKeyb.wKey.isPressed);
        bool gamePressing = (curGamep != null) && (curGamep.buttonSouth.isPressed || curGamep.buttonNorth.isPressed || curGamep.leftStick.up.isPressed || curGamep.dpad.up.isPressed);
        return keyPressing || gamePressing;
    }

    bool isPressingParry()
    {
        Keyboard curKeyb = Keyboard.current;
        Gamepad curGamep = Gamepad.current;
        bool keyPressing = (curKeyb != null) && (curKeyb.jKey.isPressed);
        bool gamePressing = (curGamep != null) && (curGamep.rightShoulder.isPressed);
        return keyPressing || gamePressing;
    }

    int getMoveInput()
    {
        Keyboard curKeyb = Keyboard.current;
        Gamepad curGamep = Gamepad.current;
        bool leftKeyPressing = (curKeyb != null) && (curKeyb.aKey.isPressed);
        bool rightKeyPressing = (curKeyb != null) && (curKeyb.dKey.isPressed);
        bool leftGamePressing = (curGamep != null) && (curGamep.leftStick.left.isPressed || curGamep.dpad.left.isPressed);
        bool rightGamePressing = (curGamep != null) && (curGamep.leftStick.right.isPressed || curGamep.dpad.right.isPressed);
        if (leftKeyPressing || leftGamePressing)
        {
            return -1;
        }
        else if(rightKeyPressing || rightGamePressing)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (!parrying && canParry && isPressingParry())
            {
                parrying = true;
                canParry = false;
                if (parryMode == 0)
                {
                    moving = false;
                    jumping = false;
                }

            }
            if (!parrying || parryMode>0)
            {

                if (isPressingJump() && grounded)
                {
                    jumping = true;
                    jumpTime = 0.0f;
                }
                if (!isPressingJump() && jumping)
                {
                    jumping = false;
                    fallSpeed = 0.0f;
                }
            }
        }
        base.Update();
        anim.SetBool("parrying", parrying);
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
            if (!parrying || parryMode>0)
            {
                int moveCon = getMoveInput();
                if (moveCon == 1)
                {
                    faceDir = true;
                    moving = true;
                    moveLeftRight(Time.fixedDeltaTime);
                }
                else if (moveCon == -1)
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

    void endParry()
    {
        parrying = false;
        canSwitch = true;
    }

    protected override void gravStep(float time)
    {
        if (!parrying || parryMode > 0)
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
                fallSpeed = 0.0f;
            }
            this.transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);
        }
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
        this.transform.position = new Vector3(0.8f * LEFT_BOUND, GROUND_LEVEL, 0.0f);
    }

    public void endSwitchWindow()
    {
        canSwitch = false;
    }
}
