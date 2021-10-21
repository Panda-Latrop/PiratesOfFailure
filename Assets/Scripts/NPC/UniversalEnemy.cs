using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemy : MonoBehaviour
{
    public Rigidbody2D rg;
    public bool alive, attacking,targetDetected;
    public float speed,restTime;    
    public Transform targetUnit;
    public Vector2 dir;
    public void Update()
    {
        if (alive)
        {
            EnemyBehavior();
        }
        else
        {
            this.enabled = false;
        }
    }
    public virtual void EnemyBehavior()
    {
        return;
    }
}

