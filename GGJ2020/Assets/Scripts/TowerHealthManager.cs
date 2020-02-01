using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private void Start()
    {
        health = Random.Range(10.0f, MaxHealth);
    }
}
