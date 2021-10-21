using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTrapLaser : MonoBehaviour {
    [System.Serializable]
    public class Size
    {
        public Sprite sprRoot;
        public Sprite sprRay;
        public float ColliderFix, sprRayFix;
    }
    public bool generate, action;
    public int size, rayLength;

    public float workTime, idleTime,addTime;
    [Range(0, 2)]
    public float timeShift;
    public Rigidbody2D rg;
    public Size[] sprSized;
    public BoxCollider2D BC2D;
    public DeathTriggerBlood DTB;
    public Transform handles;
    public SpriteRenderer rayRenderer, rootRenderer;
    public Vector2 endPos;


    public void Start()
    {
        generate = false;
    }
    void OnDrawGizmos()
    {
        if (generate)
        {
            Creating();
        }
    }
    public void Creating()
    {
        rayLength = (int)Mathf.Abs(handles.localPosition.y / 0.8f);
        endPos = new Vector2(0, rayLength * 0.8f);
        rootRenderer.sprite = sprSized[size].sprRoot;
        rayRenderer.sprite = sprSized[size].sprRay;
        {
            if (timeShift >= 0 && timeShift <= 1)
            {
                action = true;
                rayRenderer.enabled = DTB.active = true;
                DTB.tag = "Trap";
                addTime = timeShift * workTime;
            }
            else
            {
                float tmp = 2 - timeShift;
                action = false;
                rayRenderer.enabled = DTB.active = false;
                DTB.tag = "Untagged";
                addTime = tmp * idleTime;
            }
        }
        rayRenderer.size = new Vector2(sprSized[size].sprRayFix, rayLength * 0.8f);
        BC2D.gameObject.transform.localPosition = new Vector2(0, (rayLength * 0.8f) / 2f);
        BC2D.size = new Vector2(sprSized[size].ColliderFix, rayLength * 0.8f);

    }
    void Update()
    {
        if (action)
        {
            if (addTime < workTime)
            {
                rayRenderer.enabled = DTB.active = true;
                DTB.tag = "Trap";
            }
            else
            {
                addTime = 0;
                rayRenderer.enabled = DTB.active = false;
                action = false;
            }
            
        }
        else
        {
            if (addTime < idleTime)
            {
                rayRenderer.enabled = DTB.active = false;
                DTB.tag = "Untagged";
            }
            else
            {
                addTime = 0;
                rayRenderer.enabled = DTB.active = true;
                action = true;
            }
        }
        addTime += Time.deltaTime;
    }
}
