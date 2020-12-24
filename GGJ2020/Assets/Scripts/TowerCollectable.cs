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

    public override void SetClickability(ClickabilityEnum newClickability)
    {
        if (!CanClick())
        {
            Clickability = ClickabilityEnum.OutOfRange;
            return;
        }

        if (Clickability == ClickabilityEnum.Clicked && secondsOfClickLeft > 0)
        {
            secondsOfClickLeft -= Time.deltaTime;
        }
        else
        {
            Clickability = newClickability;
            outline.OutlineColor = StatusColors[newClickability];
        }
    }
}
