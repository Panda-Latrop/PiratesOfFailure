using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyAnimationRifleman : UniversalEnemyAnimation
{
    public SpriteRenderer sprRY;
    public override void Update()
    {
        base.Update();
        if (UE.alive)
            if (UE.dir.x > 0)
                sprRY.flipY = true;
            else
                sprRY.flipY = false;
    }
}
