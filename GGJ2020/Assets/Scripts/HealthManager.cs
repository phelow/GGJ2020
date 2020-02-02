using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    protected ProgressBarPro healthBar;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Pullable pullable;
    protected const float MaxHealth = 100.0f;
    protected float health = MaxHealth;

    internal virtual void TakeHit(Vector2 movementVector, float damageToTake = 10.0f)
    {
        rigidbody2D.AddForce(movementVector);
        if (IsInvulnerable())
        {
            return;
        }
        
        health -= damageToTake;
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

            OnKilled();
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnKilled()
    {
    }

    protected virtual bool IsInvulnerable()
    {
        return false;
    }

    internal float GetHealthRatio()
    {
        return health / MaxHealth;
    }
}