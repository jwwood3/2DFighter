using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private PlayerCon player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "damage")
        {
            Damage source = col.gameObject.GetComponent<Damage>();
            if (player.willParry())
            {
                if (source is Projectile)
                {
                    ((Projectile)source).reflect();
                }
                else
                {

                }
            }
            else if (source.isPiercing())
            {
                //col.gameObject.SendMessage("dealDamage", (Entity)this.gameObject.GetComponent<PlayerCon>());
            }
            else if(source is Projectile)
            {
                ((Projectile)source).setDead();
            }
            else
            {
                //source.deactivate();
            }
        }
    }
}
