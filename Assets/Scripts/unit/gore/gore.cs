using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gore : UnityPoolObject
{
    public ParticleSystem partSystem;
    void OnEnable()
    {
        partSystem.Play(true);
    }
    void OnDisable()
    {
        partSystem.Stop(true);      
    }
    void Update()
    {
        if (!partSystem.IsAlive(true))
        {
            Push();
        }
    }
}
