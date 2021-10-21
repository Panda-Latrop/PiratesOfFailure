using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTrapBob : MonoBehaviour {

    [System.Serializable]
    public struct SizeTrigger
    {
        public Sprite[] spr;
        public float colliderRadius;
    }

    public bool generate;
    public int size , angleLim, chainLength;
    public float speed, turn;
    [Range(0, 2)]
    public float timeShift;
    public Rigidbody2D rg;
    public SizeTrigger[] sizeType;
    public CircleCollider2D CC2D;//, CC2DSave;
    public SpriteRenderer sprRenderer, chainRenderer, rootRenderer;
    public Transform ball, handles;
    float time;
    Quaternion qStart, qEnd;
    void Start()
    {
        generate = false;
        time += timeShift;
        qStart = Quaternion.Euler(0, 0, turn * angleLim);
        qEnd = Quaternion.Euler(0, 0, turn * -angleLim);
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
        chainRenderer.size = new Vector2(chainRenderer.size.x, (chainLength * 1.6f));
        ball.transform.localPosition = new Vector2(0, -(chainLength * 1.6f));
        sprRenderer.sprite = sizeType[size].spr[0];
       // CC2DSave.radius = sizeType[size].colliderRadius - 0.2f;
        CC2D.radius = sizeType[size].colliderRadius;
    }
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        rg.MoveRotation(Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(time * speed) + 1.0f) / 2.0f).eulerAngles.z);
    }
 /*   public override void OnActivation()
    {
        if (!start)
        {
            sprRenderer.enabled = true;
            chainRenderer.enabled = true;
            rootRenderer.enabled = true;
            CC2D.enabled = true;
            start = true;
        }
    }
    public override void OnDeactivation()
    {
        if (start)
        {
            start = false;
            sprRenderer.enabled = false;
            chainRenderer.enabled = false;
            rootRenderer.enabled = false;
            CC2D.enabled = false;
        }
    }*/
}
