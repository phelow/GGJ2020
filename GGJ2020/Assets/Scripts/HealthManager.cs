using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private ProgressBarPro healthBar;

    [SerializeField]
    private Pullable pullable;
    protected const float MaxHealth = 100.0f;
    protected float health = MaxHealth;

    internal virtual void TakeHit()
    {
        health -= 10.0f;
        healthBar.SetValue(GetHealthRatio());

        if (health <= 0)
        {
            if (pullable != null)
            {
                lock (pullable)
                {
                    NexusPull.instance.RemovePullableObject(pullable);
                }
            }

            Destroy(this.gameObject);
        }
    }

    internal float GetHealthRatio()
    {
        return health / MaxHealth;
    }
}