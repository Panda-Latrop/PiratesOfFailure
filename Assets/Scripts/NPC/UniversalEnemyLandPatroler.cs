using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyLandPatroler : UniversalEnemy
{
    public WayPoints WP;
    public AggressionZone AZ;
    public int way, currentWayPoint;
    public float distToLose;
    Vector2 vectorOfMove;
    bool contusion = false;
    float timeAdd = 0;
    public void FixedUpdate()
    {
        if (alive)
            rg.velocity = new Vector2(vectorOfMove.x * speed, rg.velocity.y);
    }
    public override void EnemyBehavior()
    {
        if (attacking)
            contusion = true;
        if (contusion)
        {
            Attack();
            return;
        }
        else
            attacking = false;
        if (!targetDetected)
            Patrol();
        else
            ManHunt();
    }
    public void Patrol()
    {
        if (way == -1 || WP == null)
        {
            vectorOfMove = Vector2.zero;
            return;
        }
        vectorOfMove.Set(Mathf.Sign(WP.wayPoints[way].points[currentWayPoint].x - transform.localPosition.x), 0);
        dir = vectorOfMove;
        if (Vector2.Distance(transform.localPosition, WP.wayPoints[way].points[currentWayPoint]) < 2f)
        {
            if (currentWayPoint + 1 >= WP.wayPoints[way].points.Count)
                currentWayPoint = 0;
            else
                currentWayPoint++;
        }
    }
    public void ManHunt()
    {
        if (AZ != null && (targetUnit.position.x < AZ.left.position.x || targetUnit.position.x > AZ.right.position.x))
        {
            targetDetected = false;
            targetUnit = null;
            return;
        }
        float currentDist = Mathf.Abs(targetUnit.position.x - transform.position.x);
        if ((currentDist > distToLose) || (!targetUnit.gameObject.activeSelf))
        {
            targetDetected = false;
            targetUnit = null;
            return;
        }
        if (currentDist > 2f)
        {
            vectorOfMove.Set(Mathf.Sign(targetUnit.position.x - transform.position.x), 0);
        }
        else
        {
            vectorOfMove = Vector2.zero;
        }
        dir = vectorOfMove;
    }
    public void Attack()
    {
        if (timeAdd > restTime)
        {
            timeAdd = 0;
            contusion = false;
        }
        vectorOfMove = Vector2.zero;
        timeAdd += Time.deltaTime;
    }
}
