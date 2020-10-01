using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Spike : Projectile
{
    [SerializeField] private float pauseTime;
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (currentTime >= pauseTime)
        {
            base.FixedUpdate();
        }
        else
        {
            currentTime += Time.fixedDeltaTime;
        }
    }
}
