using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedBullet : UnityPoolObject
{
    public bool areaDamage;
    public int damage, force;
    public float destroyTime, timeOfImpact, explosionRadius;
    public Rigidbody2D rg;
    public string[] detectThisTags;
    protected float addTime;
    protected bool destruction, stop;
    public GameObject bulletSprR;
    public ParticleSystem trailParticle, impactParticle;

    public virtual void OnEnable()
    {
        rg.velocity = Vector2.zero;
        bulletSprR.SetActive(true);
        trailParticle.Play();
        impactParticle.Stop();

    }
    public virtual void OnDisable()
    {
        impactParticle.Stop();
        trailParticle.Stop();
        destruction = false;
        stop = false;
        rg.rotation = 0;
        addTime = 0;
    }
    public virtual void FixedUpdate()
    {
        if (!stop)
        {
            rg.AddForce(rg.transform.right * force, ForceMode2D.Impulse);
            stop = true;
        }
        if (destruction)
        {
            rg.velocity = Vector2.zero;
            if (addTime > timeOfImpact)
            {
                Push();
            }
        }
        else
        {
            if (addTime > destroyTime)
            {
                ForcedDestruction();
            }
            if (rg.gravityScale != 0)
                rg.rotation = Mathf.Atan2(rg.velocity.y, rg.velocity.x) * Mathf.Rad2Deg;
        }
        addTime += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!destruction)
            if (TagEquals(coll.tag, detectThisTags) && !coll.isTrigger && coll.gameObject.activeSelf)
            {
                ForcedDestruction();
                if (!areaDamage && coll.GetComponent<DefDestroyClass>() != null)
                    if (coll.GetComponent<DefDestroyClass>() != null)
                    coll.GetComponent<DefDestroyClass>().DamageFunc(damage);
            }
    }
    void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (TagEquals(colliders[i].tag, detectThisTags))
                if (colliders[i].GetComponent<DefDestroyClass>() != null)
                    colliders[i].GetComponent<DefDestroyClass>().DamageFunc(damage);
        }
    }
    public void SetProperty(int _damage, string killTag)
    {
        damage = _damage;
        detectThisTags[0] = killTag;
    }
    void ForcedDestruction()
    {
        if (areaDamage)
            Explosion();
        bulletSprR.SetActive(false);
        trailParticle.Stop();
        impactParticle.Play();
        addTime = 0;
        destruction = true;
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