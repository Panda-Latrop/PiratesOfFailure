using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResource : Item
{
    public GameObject root;
    public float addScore, addGold;
    public override void Action()
    {
        LevelManager.Instance.LRes.AddResource(0, (int)addScore);
        LevelManager.Instance.LRes.AddResource(1, (int)addGold);
        LevelManager.Instance.LRew.collectedItems++;
        root.SetActive(false);
        active = false;
    }
}
