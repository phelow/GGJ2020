using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private bool isInvulnerable = false;
    private void Start()
    {
        StartCoroutine(Invulnerability());
        health = Random.Range(10.0f, MaxHealth);
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
}
