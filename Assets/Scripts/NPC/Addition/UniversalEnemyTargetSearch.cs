using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyTargetSearch : MonoBehaviour
{
    public bool active = true;
    public UniversalEnemy UE;
    public Collider2D targetSearcher;
    public string[] detectThisTags;
    void OnTriggerStay2D(Collider2D coll)
    {
        if (active && UE.alive)
            if (!UE.targetDetected && TagEquals(coll.tag, detectThisTags) && !coll.isTrigger && coll.gameObject.activeSelf && targetSearcher.IsTouching(coll))
               // if (Mathf.Sign(UE.dir.x) == Mathf.Sign(coll.transform.position.x - UE.transform.position.x))
               {
                    UE.targetUnit = coll.transform;
                    UE.targetDetected = true;
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