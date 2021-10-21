using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    [System.Serializable]
    public class SizeTrigger
    {
        public Sprite[] spr;
        public float colliderRadius;
    }
    public bool generate;
    public int rotSpeed,size;
    public SizeTrigger[] sizeType;
    public CircleCollider2D CC2D;
    public SpriteRenderer sprRenderer, rootRenderer;
    public Transform rotor;
    public void Start()
    {
        generate = false;
    }
    void OnDrawGizmos()
    {
        if (generate)
            Creating();
    }
    public void Creating()
    {
        CC2D.radius = sizeType[size].colliderRadius;
        sprRenderer.sprite = sizeType[size].spr[0];
    }
    void Update()
    {
        rotor.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }
}
