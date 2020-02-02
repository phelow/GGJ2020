using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private bool isInvulnerable = false;
    public AudioSource repairSound;
    private void Start()
    {
        TakeHit(new Vector2(0, 0), Random.Range(10.0f, MaxHealth - 1.0f));
        StartCoroutine(Invulnerability());
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(10.0f);
        isInvulnerable = false;
    }

    protected override bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    public void RepairFort()
    {
        if (!ResourceTracker.instance.HasCharge())
        {
            return;
        }

        if (health == MaxHealth)
        {
            return;
        }

        Hammer.instance.SetTerminus(this.transform.position);
        ResourceTracker.instance.UseCharge();
        health = MaxHealth;
        healthBar.SetValue(GetHealthRatio());
        repairSound.Play();
    }
}
