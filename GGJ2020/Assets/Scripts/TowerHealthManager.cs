using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private bool isInvulnerable = false;
    public AudioSource repairSound;
    private void Start()
    {
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
        SetHealth(MaxHealth);
    }

    protected override void SetHealth(float healthToSet)
    {
        health = healthToSet;
        healthBar.SetValue(GetHealthRatio());
        transform.localScale = Vector3.one * 5.0f * Mathf.Lerp(0.2f, 1.5f, GetHealthRatio());
        repairSound.Play();
    }

    internal void SpawnTower(GameObject spawnableTower)
    {
        float targetHealth = health / 2.0f;
        
        GameObject.Instantiate(spawnableTower, transform.position, transform.rotation, null).GetComponent<TowerHealthManager>().TakeHit(new Vector2(0,0),MaxHealth - targetHealth);
        this.TakeHit(new Vector2(0, 0), health - targetHealth);
        repairSound.Play();
    }
}
