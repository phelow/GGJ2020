using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealthManager : HealthManager
{
    private void Start()
    {
        TakeHit(Random.Range(10.0f, MaxHealth - 1.0f));
    }
}
