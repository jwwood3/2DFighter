using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected int delaying;
    [SerializeField] protected PlayerCon player;
    [SerializeField] protected GameObject powerUp;
    // Start is called before the first frame update
    protected override void Start()
    {
        player = PlayerCon.player;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(alive && Health <= 0)
        {
            GameObject.Instantiate(powerUp, new Vector3(this.transform.position.x, 0.0f, 0.0f), Quaternion.identity);
        }
        base.Update();
    }

    public override bool isTargeted(int t)
    {
        return t == -1;
    }

    protected void delay()
    {
        delaying--;
        if (delaying <= 0)
        {
            delaying = 0;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

    }

    public override void Reset()
    {
        base.Reset();
        delaying = 0;
        this.transform.position = new Vector3(0.8f*H_BOUND,GROUND_LEVEL, 0.0f);
    }
}
