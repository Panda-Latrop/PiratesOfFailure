using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyLandRifleman : UniversalEnemy
{
    public int bulletType, damage;
    public float distToLose, rotationSpeed, shootTime, angle, angelFix;
    public Transform gunRoot, shootPoint;
    float addShootTime = 0, restAngle;
    public void Start()
    {
        restAngle = gunRoot.eulerAngles.z;
    }
    public override void EnemyBehavior()
    {
        if (!targetDetected)
            Idle();
        else
            ManHunt();
        if ((angle < 270 && angle >= 90) || (angle < -90 && angle >= -270))
        {
            dir.Set(1f, 0f);
        }
        else
        {
            dir.Set(-1f, 0f);
        }
    }
    public void Idle()
    {
        angle = 0;
        Quaternion qt = Quaternion.Euler(new Vector3(0, 0, restAngle));
        gunRoot.rotation = Quaternion.RotateTowards(gunRoot.rotation, qt, Time.deltaTime * rotationSpeed);
        addShootTime = 0;
    }
    public void ManHunt()
    {
        float currentDist = Mathf.Abs(targetUnit.position.x - transform.position.x);
        if (currentDist > distToLose)
        {
            targetDetected = false;
            targetUnit = null;
            return;
        }
        Vector3 vectorToTarget = targetUnit.transform.position - gunRoot.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - angelFix;
        Quaternion qt = Quaternion.Euler(new Vector3(0, 0, angle));
        gunRoot.rotation = Quaternion.RotateTowards(gunRoot.rotation, qt, Time.deltaTime * rotationSpeed);
        ShootBullet();
    }
    public void ShootBullet()
    {
        if (addShootTime > shootTime)
        {
            UnityPoolObject obj = UnityPoolManager.Instance.Pop<UnityPoolObject>(bulletType);
            (obj as ExtendedBullet).SetProperty(damage, "unit");
            obj.SetTransform(shootPoint.position, Quaternion.Euler(new Vector3(0, 0, shootPoint.rotation.eulerAngles.z + angelFix)), Vector3.one);
            addShootTime = 0;
        }
        addShootTime += Time.deltaTime;
    }
}