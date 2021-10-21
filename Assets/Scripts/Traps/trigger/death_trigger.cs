using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_trigger : MonoBehaviour
{
    public string destroyTag;
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target != null)
        {
                if (target.GetComponent<DefDestroyClass>() != null)
                {
                    if (target.gameObject.tag == destroyTag || destroyTag == "")
                    {
                        Kill(target.GetComponent<DefDestroyClass>());
                    }
                }           
        }
    }

    public virtual void Kill(DefDestroyClass obj)
    {
        obj.DestroyFunc();
    }
}
