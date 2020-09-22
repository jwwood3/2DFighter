﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected int delaying;
    [SerializeField] protected PlayerCon player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        if (col.gameObject.tag == "damage")
        {
            col.gameObject.SendMessage("dealDamage", (Entity)this.gameObject.GetComponent<Enemy>());
        }
    }

    public override void Reset()
    {
        base.Reset();
        delaying = 0;
        this.transform.position = new Vector3(0.8f*RIGHT_BOUND,GROUND_LEVEL, 0.0f);
    }
}
