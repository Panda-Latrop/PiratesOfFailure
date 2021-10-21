using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyAttackCollision : MonoBehaviour
{
    public bool active = true, setDir = true;
    public UniversalEnemy UE;
    public Collider2D attackZone;
    public string[] detectThisTags;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (active && UE.alive)
            if (TagEquals(coll.tag, detectThisTags) && !coll.isTrigger && attackZone.IsTouching(coll))
                if (coll.GetComponent<DefDestroyClass>() != null)
                {
                    UE.attacking = true;
                    if (setDir)
                        UE.dir = (coll.transform.position - transform.position).normalized;
                    coll.GetComponent<DefDestroyClass>().DestroyFunc();
                }
    }
    bool TagEquals(string gojT, string[] arrT)
    {
        for (int i = 0; i < arrT.Length; i++)
            if (gojT == arrT[i])
                return true;
        return false;
    }
}
