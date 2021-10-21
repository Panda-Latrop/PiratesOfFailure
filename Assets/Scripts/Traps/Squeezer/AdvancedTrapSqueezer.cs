using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTrapSqueezer : MonoBehaviour
{
    [System.Serializable]
    public struct SizeTrigger
    {
        public Sprite exite, rob;
        public float robFix;
        public Sprite[] spr;            
        public Vector2 colliderSize;
    }
    public bool generate,pressing;
    public int rodLength, size;
    public float speed,accelerating, returnSpeed, idleTimeAfPress, idleTimeBfPress;
    [Range(0, 2)]
    public float timeShift;
    public Rigidbody2D rg;
    public SizeTrigger[] sizeType;
    public BoxCollider2D BC2D, BC2DSave;
    public DeathTriggerBlood DTB;
    public SpriteRenderer sprRenderer,robRenderer,rootRenderer;
    public Transform handles;
    public Vector2 currentPos, endPos, startPos;
    float time, acceleratingAdd;

    void Start()
    {
        generate = false;
    }
    void OnDrawGizmos()
    {
        if (generate)
        {
            Creating();
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(transform.position.x, endPos.y + transform.position.y, 0), 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(transform.position.x, endPos.y - 1.6f + transform.position.y, 0), 0.5f);
        }
    }
    void Creating()
    {
        rodLength = (int)Mathf.Abs(handles.localPosition.y / 1.6f);
        if (rodLength < 1)
            rodLength = 1;
        endPos = new Vector2(0, -(rodLength * 1.6f));
        startPos = new Vector2(0, -1.6f);
        sprRenderer.sprite = sizeType[size].spr[0];
        {
            if (timeShift >= 0 && timeShift <= 1)
            {
                pressing = true;
                currentPos = new Vector2(0, -1.6f + timeShift * (endPos.y + 1.6f));
            }
            else
            {
                float tmp = 2 - timeShift;
                pressing = false;
                currentPos = new Vector2(0, -1.6f + tmp * (endPos.y + 1.6f));
            }
        }
        BC2D.offset = new Vector2(0, -sizeType[size].colliderSize.y/2);
        BC2D.size = new Vector2(sizeType[size].colliderSize.x - 0.1f,1f);
        BC2DSave.size = sizeType[size].colliderSize;
        robRenderer.size = new Vector2(sizeType[size].robFix, -currentPos.y);
        robRenderer.sprite = sizeType[size].rob;
        rootRenderer.sprite = sizeType[size].exite;
        rg.transform.localPosition = new Vector2(0, currentPos.y);
    }
    void Update()
    {
        robRenderer.size = new Vector2(robRenderer.size.x, transform.position.y - rg.transform.position.y);
    }
    void FixedUpdate()
    {
        rg.MovePosition(transform.position + (Vector3)currentPos);
        DTB.active = (pressing && (currentPos.y > endPos.y));
         if (pressing)
        {
            if (currentPos.y > endPos.y)
            {
                currentPos = Vector2.MoveTowards(currentPos, endPos, (speed + acceleratingAdd) * Time.fixedDeltaTime);
                acceleratingAdd += accelerating;
            }
            else
            {
                currentPos = new Vector2(0, endPos.y);
                acceleratingAdd = 0;
                IdleTime();
            }

        }
        else
         {       
            if (currentPos.y < startPos.y)
            {
                currentPos = Vector2.MoveTowards(currentPos, startPos, returnSpeed * Time.fixedDeltaTime);
            }
            else
            {
                currentPos = new Vector2(0, startPos.y);
                IdleTime();
            }
        }     
    }
    void IdleTime()
    {
        time += Time.fixedDeltaTime;
        if (pressing)
        {
            if(time >= idleTimeAfPress)
            {
                time = 0;
                pressing = false;
                return;
            }
            return;
        }
        else
        {
            if (time >= idleTimeBfPress)
            {                          
                time = 0;
                pressing = true;
                return;
            }
            return;
        }
    }

}
