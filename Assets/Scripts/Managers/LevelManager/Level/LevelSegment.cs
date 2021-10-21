using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    public bool unitHere, unitNear, active;
    public GameObject root;
    public Transform left, right;
    public int idInArr = -1;
   /* public void Start()
    {
        traps = trapContainer.GetComponentsInChildren<DefTrap>();
        npcs = npcContainer.GetComponentsInChildren<NPCEnemyDef>();
    }*/
    void Update()
    {
        UnitCheck();
        if (active)
        {
            Activation();
        }
        else
        {
            Deactivation();
        }
        Action();
    }
    public void UnitCheck()
    {
        unitHere = (Camera.main.transform.position.x < right.position.x + 3.2f && Camera.main.transform.position.x > left.position.x - 3.2f);
        if (unitHere)
        {
            LevelManager.Instance.LS.UnitSegmentLocationCheck(idInArr);
            active = true;
        }
        if (unitNear)
        {
            LevelManager.Instance.LS.SegmentSelfCheck(idInArr);
            active = true;
        }
        if (active && !unitHere && !unitNear)
        {
            active = false;
        }
    }
    public virtual void Action()
    {
        return;
    }
    public virtual void Activation()
    {
        root.SetActive(true);
    }
    public virtual void Deactivation()
    {
        root.SetActive(false);
    }
}
