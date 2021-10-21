using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerBlood : MonoBehaviour
{
    public bool active = true;
    public int killCount;
    public string[] detectThisTags;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (active)
            if (TagEquals(coll.tag, detectThisTags))
                if (coll.GetComponent<DefDestroyClass>() != null)
                {
                    coll.GetComponent<DefDestroyClass>().DestroyFunc();
                    killCount++;
                }
    }
    bool TagEquals(string gojT, string[] arrT)
    {
        for (int i = 0; i < arrT.Length; i++)
        {
            if (gojT == arrT[i])
                return true;
        }
        return false;
    }
}
