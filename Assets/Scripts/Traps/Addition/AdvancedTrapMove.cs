using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTrapMove : MonoBehaviour
{
    public Rigidbody2D rg;
    public bool onPosition, rightMove = true;
    public int speed, currentPoint;
    public float idleTime;
    public Transform[] wayPoints;
    public Vector2 currentPos;
    float addTime;
    void FixedUpdate()
    {
      
        if (Vector2.Distance(currentPos, wayPoints[currentPoint].localPosition) > 0.1f)
        {
            currentPos = Vector2.MoveTowards(currentPos, wayPoints[currentPoint].localPosition, speed * Time.fixedDeltaTime);
            rg.MovePosition(transform.position + (Vector3)currentPos);
        }
        else
        {
            if (idleTime == 0)
            {
                NextPoint();            
            }
            else
            {
                IdleTime();
            }
        }
    }

    void IdleTime()
    {
        addTime += Time.fixedDeltaTime;
        if (addTime >= idleTime)
        {
            addTime = 0;
            onPosition = false;
            NextPoint();
            return;
        }
        return;
    }
    void NextPoint()
    {
        if (rightMove)
        {
            if (currentPoint + 1 >= wayPoints.Length)
            {
                currentPoint--;
                rightMove = false;
            }
            else
            {
                currentPoint++;
            }
        }
        else
        {
            if (currentPoint - 1 < 0)
            {
                currentPoint++;
                rightMove = true;
            }
            else
            {
                currentPoint--;
            }
        }
    }
}
