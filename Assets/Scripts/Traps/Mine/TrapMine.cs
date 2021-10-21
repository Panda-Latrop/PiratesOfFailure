using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMine : DefDestroyClass
{
    public int damage;
    public float explosionRadius;
    public string[] detectThisTags;
    public Collider2D collider2d;
    public SpriteRenderer sprRenderer;
    public Sprite deadMine;//,liveMine;
    public ParticleSystem particle;
    public Transform pos;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (TagEquals(target.tag, detectThisTags))
            DamageFunc(damage);
    }
    public override void DestroyFunc()
    {
        particle.Play();
        collider2d.enabled = false;
       // sprRenderer.SetActive(false);
      //  sprRenderer.enabled = false;
        sprRenderer.sprite = deadMine;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (TagEquals(colliders[i].tag, detectThisTags))
                if (colliders[i].GetComponent<DefDestroyClass>() != null)
                {
                    colliders[i].GetComponent<DefDestroyClass>().DamageFunc(damage);
                }
        }   
    }

    bool TagEquals(string gojT, string[] arrT)
    {
        for (int i = 0; i < arrT.Length; i++)
        {
            if (gojT == arrT[i])
                return true;
        }
        return false;
    }
}
