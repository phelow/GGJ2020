using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableOnDamaged : Pullable
{
    [SerializeField]
    private HealthManager healthManager;

    protected override float GetPullForce(float distance)
    {
        const float MaxForce = 5.0f;
        const float MinForce = .1f;

        const float MaxDistance = 20.0f;
        const float MinDistance = 1.0f;

        const float DistanceExponent = 5.0f;
        return Mathf.Lerp(MaxForce, MinForce, Mathf.InverseLerp(MinDistance, MaxDistance, Mathf.Pow(distance, DistanceExponent)) / healthManager.GetHealthRatio());
    }
}
