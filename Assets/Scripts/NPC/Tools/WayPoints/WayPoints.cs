using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

    [System.Serializable]   
    public class Way
    {
        public bool quadraticCurve,loop;
        public float step;
        public Transform[] mainPoints;
        public List<Vector2> points = new List<Vector2>();
    }

    public bool draw, generatePoints;
    public Way[] wayPoints;

    void Start()
    {
        draw = generatePoints = false;
    }

    void OnDrawGizmos()
    {       
       if (generatePoints)
        {
            GeneratePoints();
          //  generatePoints = false;
        }
       if (draw)
       {
           GizmosDrawPoints();
       }
    }
    public void GizmosDrawPoints()
    {
        Gizmos.color = Color.white;
        if (wayPoints.Length < 1)
            return;

        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (wayPoints[i].points.Count < 2)              
                return;

            Vector2 pos = wayPoints[i].points[0];
            Gizmos.DrawSphere(pos, 0.5f);
            for (int k = 1; k < wayPoints[i].points.Count; k++)
            {
                pos = wayPoints[i].points[k];
                Vector2 pref = wayPoints[i].points[k - 1];
                Gizmos.DrawLine(pref, pos);
                Gizmos.DrawSphere(pos, 0.2f);
            }
            if (wayPoints[i].loop)
            {
                Gizmos.DrawLine(wayPoints[i].points[wayPoints[i].points.Count - 1], wayPoints[i].points[0]);
            }
            if (wayPoints[i].mainPoints[0].childCount != 0)
            {

                for (int k = 0; k < wayPoints[i].mainPoints.Length; k++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(wayPoints[i].mainPoints[k].GetChild(0).position, wayPoints[i].mainPoints[k].position);
                    Gizmos.DrawLine(wayPoints[i].mainPoints[k].GetChild(1).position, wayPoints[i].mainPoints[k].position);
                    Gizmos.DrawSphere(wayPoints[i].mainPoints[k].GetChild(0).position, 0.2f);
                    Gizmos.DrawSphere(wayPoints[i].mainPoints[k].GetChild(1).position, 0.2f);
                    wayPoints[i].mainPoints[k].GetChild(1).localPosition = -wayPoints[i].mainPoints[k].GetChild(0).localPosition;
                    Gizmos.color = Color.white;
                }
            }
            
        }
    }
    public void GeneratePoints()
    {
        
        for (int i = 0; i < wayPoints.Length; i++)
        {
          /*  if (wayPoints[i].points.Count > 10)
            {
                return;
            }*/
            wayPoints[i].points.Clear();
            if (wayPoints[i].quadraticCurve && wayPoints[i].mainPoints.Length > 2)
            {
                if (wayPoints[i].mainPoints[0].childCount == 0)
                {
                    for (int k = 0; k < wayPoints[i].mainPoints.Length; k++)
                    {
                        GameObject left = new GameObject();
                        left.name = "left" + k.ToString();
                        left.transform.position = new Vector2(wayPoints[i].mainPoints[k].position.x - 5, wayPoints[i].mainPoints[k].position.y);
                        GameObject right = new GameObject();
                        right.name = "right" + k.ToString();
                        right.transform.position = new Vector2(wayPoints[i].mainPoints[k].position.x + 5, wayPoints[i].mainPoints[k].position.y);
                        left.transform.parent = wayPoints[i].mainPoints[k];
                        right.transform.parent = wayPoints[i].mainPoints[k];
                    }
                } 
                Vector2 b0, b1, b2, q0, q1, z0;
                for (int k = 1; k < wayPoints[i].mainPoints.Length; k++)
                {
                    if (wayPoints[i].step > 1 || wayPoints[i].step <= 0)
                    {
                        wayPoints[i].step = 0.25f;
                    }
                    
                    for (float t = 0; t <= 1; t += wayPoints[i].step)
                    {

                        b0 = GetPoint(wayPoints[i].mainPoints[k - 1].position, wayPoints[i].mainPoints[k - 1].GetChild(1).position, t);
                        b1 = GetPoint(wayPoints[i].mainPoints[k - 1].GetChild(1).position, wayPoints[i].mainPoints[k].GetChild(0).position, t);
                        b2 = GetPoint(wayPoints[i].mainPoints[k].GetChild(0).position, wayPoints[i].mainPoints[k].position, t);
                        q0 = GetPoint(b0, b1, t);
                        q1 = GetPoint(b1, b2, t);
                        z0 = GetPoint(q0, q1, t);
                        wayPoints[i].points.Add(z0);
                    }
                }
                if (wayPoints[i].loop)
                {
                    for (float t = 0; t < 1; t += wayPoints[i].step)
                    {
                        b0 = GetPoint(wayPoints[i].mainPoints[wayPoints[i].mainPoints.Length - 1].position, wayPoints[i].mainPoints[wayPoints[i].mainPoints.Length - 1].GetChild(1).position, t);
                        b1 = GetPoint(wayPoints[i].mainPoints[wayPoints[i].mainPoints.Length - 1].GetChild(1).position, wayPoints[i].mainPoints[0].GetChild(0).position, t);
                        b2 = GetPoint(wayPoints[i].mainPoints[0].GetChild(0).position, wayPoints[i].mainPoints[0].position, t);
                        q0 = GetPoint(b0, b1, t);
                        q1 = GetPoint(b1, b2, t);
                        z0 = GetPoint(q0, q1, t);
                        wayPoints[i].points.Add(z0);
                    }
                }
            }
            else
            {
                for (int k = 0; k < wayPoints[i].mainPoints.Length; k++)
                {
                    wayPoints[i].points.Add(wayPoints[i].mainPoints[k].position);
                  /*  if (wayPoints[i].mainPoints[k].childCount != 0)
                    {
                        wayPoints[i].mainPoints[k].DetachChildren();
                    }*/
                }
            }
        }
    }


    

    Vector2 GetPoint(Vector2 p0, Vector2 p1, float _t)
    {
        Vector2 _b = new Vector2(p0.x + _t * (p1.x - p0.x), p0.y + _t * (p1.y - p0.y));
        return _b;
    }
}
