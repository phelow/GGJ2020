using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    protected Collectable collectable;

    [SerializeField]
    protected ProgressBarPro healthBar;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Pullable pullable;
    protected const float MaxHealth = 100.0f;
    [SerializeField]
    protected float health = MaxHealth;

    internal virtual void TakeHit(Vector2 movementVector, float damageToTake = 15.0f)
    {
        rigidbody2D.AddForce(movementVector);
        
        
        health -= damageToTake;
        SetHealth(health);

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

    protected virtual void SetHealth(float health)
    {

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