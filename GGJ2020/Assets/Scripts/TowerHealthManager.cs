using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private bool isInvulnerable = false;
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

        ResourceTracker.instance.UseCharge();
        health = Mathf.Max(health + 30.0f, MaxHealth);
        healthBar.SetValue(GetHealthRatio());
    }
}
