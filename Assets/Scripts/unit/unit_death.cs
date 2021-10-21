using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_death : DefDestroyClass
{
    public int goreTag;
    public Vector2 gorepos;
    public unit_movement uM;
    int maxHealth;
    void OnDisable()
    {
        currentHealth = health;
    }
    public override void DestroyFunc()
    {
        gorepos = transform.position;
        UnityPoolObject obj = UnityPoolManager.Instance.Pop<UnityPoolObject>(goreTag);
        obj.SetTransform(gorepos, Quaternion.identity, Vector3.one);
        groupManager.Instance.UnitsDeath(uM);
        uM.Push();
        transform.localPosition = Vector2.zero;
    }
}
