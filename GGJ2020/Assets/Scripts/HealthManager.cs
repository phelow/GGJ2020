using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private ProgressBarPro healthBar;

    [SerializeField]
    private Pullable pullable;
    private const float MaxHealth = 100.0f;
    private float health = MaxHealth;

    internal virtual void TakeHit()
    {
        health -= 10.0f;
        healthBar.SetValue(health/MaxHealth);

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
}