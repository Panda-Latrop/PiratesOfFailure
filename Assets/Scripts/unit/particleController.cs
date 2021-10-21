using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour
{

    public ParticleSystem[] particleSystems;

    public int particleType;

    public void SetParticle(int _particleType)
    {
        particleType = _particleType;
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
            particleSystems[i].Clear(false);
        }
    }
    public void EmiteParticle()
    {
      //  if (!particleSystems[particleType].isPlaying)
      //  {
            particleSystems[particleType].Play();
       // }
    }
    public void StopParticle()
    {
            particleSystems[particleType].Stop();
            particleSystems[particleType].Clear(false);
    }
}
