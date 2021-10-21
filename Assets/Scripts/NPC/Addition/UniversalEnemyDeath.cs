using UnityEngine;

public class UniversalEnemyDeath : DefDestroyClass
{
    public UniversalEnemy UE;
    public GameObject healthBar, root;
    public SpriteRenderer healthLine;
    public ParticleSystem deathParticle;
    public float addScore,addGold;
    public override void DamageFunc(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            healthBar.SetActive(false);
            DestroyFunc();
        }
        else
        {
            healthBar.SetActive(true);
            healthLine.size = new Vector2(((float)currentHealth / (float)health) * 3, 0.4f);
        }
    }
    public override void DestroyFunc()
    {
        UE.alive = false;
        UE.rg.velocity = Vector2.zero;
        root.SetActive(false);
        deathParticle.Play();

        LevelManager.Instance.LRes.AddResource(0, (int)addScore);
        LevelManager.Instance.LRes.AddResource(1, (int)addGold);
        LevelManager.Instance.LRew.enemyKilled++;
    }
}