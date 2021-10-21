using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyAnimation : MonoBehaviour
{
    public UniversalEnemy UE;
    public Animator anim;
    public SpriteRenderer sprRX;
    public virtual void Update()
    {
        if (UE.alive)
        {
            if (UE.dir.x > 0)
                sprRX.flipX = true;
            else
                sprRX.flipX = false;
            if (anim != null)
            {
                if (UE.rg.velocity.x != 0)
                    anim.SetBool("walk", true);
                else
                    anim.SetBool("walk", false);
                if (UE.attacking)
                {
                    anim.SetTrigger("Attack");
                    UE.attacking = false;
                }
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}
  //  