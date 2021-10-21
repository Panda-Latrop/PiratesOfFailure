using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_smooth : MonoBehaviour {
    public Vector3 target, offset, shift = Vector3.zero;
    public Vector2 scope;
    public float smoothTime = 0.3F;//, addHorizontal;
    private Vector3 fp = Vector3.zero, lp = Vector3.zero;//Vector3 velocity = Vector3.zero,

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(target, 0.2f);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawLine(transform.position - (Vector3)scope, transform.position + (Vector3)scope);
    }

    void LateUpdate()
    {
        
       
        if (groupManager.Instance.group.Count != 0)
        {
            target = groupManager.Instance.group[0].gameObject.transform.position + offset;
        }
            if (target.x < transform.position.x - scope.x)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x + scope.x,  smoothTime * Time.deltaTime), transform.position.y, transform.position.z);
               // transform.position.Set(Mathf.Lerp(transform.position.x, target.x ,  smoothTime * Time.deltaTime),transform.position.y,transform.position.z);
            }
            if (target.x > transform.position.x + scope.x)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x - scope.x, smoothTime * Time.deltaTime), transform.position.y, transform.position.z);
            }
            if (target.y < transform.position.y - scope.y )
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, target.y + scope.y,  smoothTime * Time.deltaTime), transform.position.z);
            }
            if (target.y > transform.position.y + scope.y)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, target.y - scope.y, smoothTime * Time.deltaTime), transform.position.z);
            }

      //  transform.position = Vector3.Lerp(transform.position, target , smoothTime * Time.deltaTime);
      //  transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.x, target.y + addVertical, -10), ref velocity, smoothTime);
        fp = transform.position;
        shift = (fp - lp);        
        lp = transform.position;
        

    }
}
