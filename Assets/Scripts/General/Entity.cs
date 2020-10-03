using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float fallSpeed;
    [SerializeField] protected float fallAccel;
    [SerializeField] protected bool grounded;
    [SerializeField] protected float GROUND_LEVEL;
    [SerializeField] protected bool jumping;
    [SerializeField] protected bool resetting;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected float jumpSpeed;
    [SerializeField] protected float MAX_JUMP_TIME;
    [SerializeField] protected bool faceDir;
    [SerializeField] protected int faceUp;
    [SerializeField] protected bool moving;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int Health;
    [SerializeField] protected bool alive = true;
    [SerializeField] protected float H_BOUND;
    [SerializeField] protected float TOP_BOUND;
    [SerializeField] public Animator anim;
    [SerializeField] protected bool invulnerable;
    [SerializeField] protected bool got_hit;
    [SerializeField] protected Vector3 target;
    [SerializeField] protected float positionCheck;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        grounded = this.transform.position.y == GROUND_LEVEL;
        alive = this.Health != 0;
        anim.SetBool("resetting", resetting);
        anim.SetBool("jumping", jumping);
        anim.SetBool("grounded", grounded);
        anim.SetBool("facingRight", faceDir);
        anim.SetBool("moving", moving);
        anim.SetBool("alive", alive);
        anim.SetBool("got_hit", got_hit);
        anim.SetInteger("facingVert", faceUp);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (jumping)
        {
            jumpStep(Time.fixedDeltaTime);
        }
        else
        {
            gravStep(Time.fixedDeltaTime);
        }
    }

    void stopGettingHit()
    {
        got_hit = false;
    }
    
    public virtual void takeDamage(int dmg)
    {
        if (!invulnerable)
        {
            Health -= dmg;
            got_hit = true;
        }
    }

    protected virtual void moveLeftRight(float time)
    {

        int dir;
        if (faceDir) { dir = 1; } else { dir = -1; }
        float newX = this.transform.position.x + (dir * time * walkSpeed);
        if (newX > H_BOUND)
        {
            newX = H_BOUND;
            moving = false;
        }
        if (newX < -H_BOUND)
        {
            newX = -H_BOUND;
            moving = false;
        }
        this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
    }

    protected virtual void gravStep(float time)
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
            fallSpeed = 0.0f;
        }
        this.transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);
    }

    protected virtual void jumpStep(float time)
    {
        jumpTime += time;
        if (jumpTime >= MAX_JUMP_TIME)
        {
            jumping = false;
            fallSpeed = 0.0f;
            return;
        }
        float jumpBit = ((-(jumpSpeed * jumpTime / (MAX_JUMP_TIME)) + jumpSpeed) + (jumpSpeed / 8.0f)) * time;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + jumpBit, this.transform.position.z);

    }
    
    public virtual bool isTargeted(int t)
    {
        return true;
    }

    public virtual int getHealth()
    {
        return Health;
    }

    public virtual int getMaxHealth()
    {
        return MaxHealth;
    }

    public virtual void finishReset()
    {
        resetting = false;
    }

    public virtual void Reset()
    {
        Health = MaxHealth;
        this.transform.position = new Vector3(0.0f, GROUND_LEVEL, 0.0f);
        jumping = false;
        moving = false;
        grounded = true;
        faceDir = true;
        alive = true;
        got_hit = false;
        resetting = true;
    }

    protected void moveTowards(Vector3 pos, float t)
    {
        faceDir = transform.position.x < pos.x;
        faceUp = transform.position.y + 5 < pos.y ? 1 : (transform.position.y - 5 > pos.y ? -1 : 0);
        Vector3 dir = Vector3.Normalize(new Vector3(pos.x - transform.position.x, pos.y - transform.position.y, pos.z - transform.position.z))*walkSpeed;
        transform.position = new Vector3(transform.position.x + (dir.x * t), transform.position.y + (dir.y * t), transform.position.z + (dir.z * t));
    }

    protected bool isAt(Vector3 pos)
    {
        return new Vector3(pos.x - transform.position.x, pos.y - transform.position.y, pos.z - transform.position.z).magnitude < positionCheck;
    }

    public float getGround()
    {
        return GROUND_LEVEL;
    }

    public float getSky()
    {
        return TOP_BOUND;
    }

    public float getHBound()
    {
        return H_BOUND;
    }
}
