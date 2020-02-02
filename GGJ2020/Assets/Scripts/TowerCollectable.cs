using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollectable : Collectable
{
    [SerializeField]
    private HealthManager healthManager;
    public override bool CanClick()
    {
        return healthManager.GetHealthRatio() != 1.0f;
    }
}
