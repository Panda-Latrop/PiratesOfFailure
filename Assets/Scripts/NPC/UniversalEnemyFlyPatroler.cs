using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyFlyPatroler : UniversalEnemy
{
    public WayPoints WP;
    public int way, currentWayPoint;
    public UniversalEnemyAttackCollision UEAC;
    Vector2 vectorOfMove;
    protected float timeAdd = 0;
    public void FixedUpdate()
    {
        if (alive)
            rg.velocity = vectorOfMove * speed;
    }
    public override void EnemyBehavior()
    {
        if (attacking)
        {
            UEAC.active = false;
            Attack();
        }
       Patrol();

    }
    public void Patrol()
    {
        if (way == -1 || WP == null)
        {
            vectorOfMove = Vector2.zero;
            return;
        }
        dir = vectorOfMove = (WP.wayPoints[way].points[currentWayPoint] - (Vector2)transform.localPosition).normalized;
        if (Vector2.Distance(transform.localPosition, WP.wayPoints[way].points[currentWayPoint]) < 2f)
        {
            if (currentWayPoint + 1 >= WP.wayPoints[way].points.Count)
                currentWayPoint = 0;
            else
                currentWayPoint++;
        }
    }
    
    public void Attack()
    {
        if (timeAdd > restTime)
        {
            timeAdd = 0;          
            attacking = false;
            UEAC.active = true;
        }
        timeAdd += Time.deltaTime;
    }
}
