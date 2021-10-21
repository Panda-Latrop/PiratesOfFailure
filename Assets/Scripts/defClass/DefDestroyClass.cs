using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefDestroyClass : MonoBehaviour 
{

    public int health;
    protected int currentHealth;
    public virtual void Start()
    {
         currentHealth = health;
    }
    public virtual void DamageFunc(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            DestroyFunc();
    }
    public virtual void DestroyFunc()
    {
        print("No action");
    }
}
    