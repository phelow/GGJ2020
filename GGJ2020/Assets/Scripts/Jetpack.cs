using System.Collections;
using UnityEngine;


public class Jetpack : MonoBehaviour
{
    private ParticleSystem mParticleSystem;

    void Awake()
    {
        mParticleSystem = GetComponent<ParticleSystem>();
    }

    public void setState(bool state)
    {
        if (mParticleSystem.emission.enabled != state)
            mParticleSystem.enableEmission = state;
       
    }
}
