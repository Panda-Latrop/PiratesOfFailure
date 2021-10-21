using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegmentSpawn : LevelSegment
{
    public Transform spawnPoint;
    public float timeToSpawn;
    public bool activeSpawnPoint;
    public override void Action()
    {
        if (unitHere && !activeSpawnPoint)
        {
            LevelManager.Instance.LS.UdateSpawnPoint(this);
            activeSpawnPoint = true;
        }
    }
 /*   public override void Activation()
    {
        roo.SetActive(true);
    }
    public override void Deactivation()
    {
        platforms.SetActive(false);
    }*/
}
