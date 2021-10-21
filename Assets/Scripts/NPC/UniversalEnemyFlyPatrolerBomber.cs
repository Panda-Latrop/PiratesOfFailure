using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyFlyPatrolerBomber : UniversalEnemyFlyPatroler
{
    public Transform shootPoint;
    public int bulletType, damage;
    public float timeToShoot;
    bool shootBullet = false;
    public override void EnemyBehavior()
    {
        base.EnemyBehavior();
        if (targetDetected)
        {
            if (!shootBullet)
            {
                UnityPoolObject obj = UnityPoolManager.Instance.Pop<UnityPoolObject>(bulletType);
                (obj as ExtendedBullet).SetProperty(damage, "unit");
                obj.SetTransform(shootPoint.position, Quaternion.identity, Vector3.one);
                shootBullet = true;
            }
            if (timeAdd > timeToShoot)
            {
                timeAdd = 0;
                targetDetected = false;
                shootBullet = false;
            }
            timeAdd += Time.deltaTime;
        }
    }
}
