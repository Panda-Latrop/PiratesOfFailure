using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedScythe : MonoBehaviour
{

    [System.Serializable]
    public struct SizeTrigger
    {
        public Sprite[] spr;
        public Vector2 colliderSize;
    }

    public bool generate;
    public int size, chainLength;
    public float speed = 100f, angle,accel, accelMin = 0.5f, accelMax = 1.5f;
    [Range(0, 2)]
    public float timeShift;
    public Rigidbody2D rg;
    public SizeTrigger[] sizeType;
    public BoxCollider2D BC2D;//, BC2DSave;
    public SpriteRenderer scytheRenderer, robRenderer, rootRenderer;
    public Transform scythe, handles;
    float timeAdd, accelAdd;
    void Start()
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
    void Creating()
    {
        chainLength = (int)Mathf.Abs(handles.localPosition.y / 1.6f);
        if (chainLength < 1)
            chainLength = 1;
        robRenderer.size = new Vector2(robRenderer.size.x, (chainLength * 1.6f));
        scythe.transform.localPosition = new Vector2(0, -(chainLength * 1.6f));
        scytheRenderer.sprite = sizeType[size].spr[0];
      //  BC2DSave.size = sizeType[size].colliderSize - Vector2.one * 0.2f;
        BC2D.size = sizeType[size].colliderSize;

        if (timeShift >= 0 && timeShift <= 1)
        {
            angle = timeShift * 180;
            accel = accelMax * (1 - timeShift) +  timeShift*accelMin;
        }
        else
        {
            angle = (timeShift-2) * 180;
            accel = accelMin * (1 - (timeShift-1)) + ( timeShift-1) * accelMax;
        }

        rg.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    void Update()
    {
        accelAdd = speed * 0.0001f;

        if (angle >= 180)
        {
            angle *= -1;
        //    print(accel);
        }

        if (angle > 0)
        {
            if (accel > 0.5f)
            {
                accel -= accelAdd;
            }
            else
            {
                accel = 0.5f;
            }
        }
        else
        {
            if (accel < 1.5f)
            {
                accel += accelAdd;
            }
            else
            {
                accel = 1.5f;
            }
        }
    }
    void FixedUpdate()
    {
        angle += Time.fixedDeltaTime * speed * accel;
        rg.MoveRotation(angle);
    }
}
