using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyMiniBossGate : MonoBehaviour {

    public SpriteRenderer gateSpr;
    public Collider2D gateCollider;
    public UniversalEnemy[] turrets;
	void Update () 
    {
        if (!TurretsAlive())
        {
            gateSpr.enabled = true;
            gateCollider.enabled = false;
            this.enabled = false;
        }
	}
    public bool TurretsAlive()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i].alive)
                return true;
        }
        return false;
    }
}
