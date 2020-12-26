﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollectable : Collectable
{
    [SerializeField]
    private HealthManager healthManager;

    [SerializeField]
    private Rigidbody2D rigidbody;
    public override bool CanClick()
    {
        return healthManager.GetHealthRatio() <= .9f && ResourceTracker.instance.HasCharge();
    }

    internal override bool CanRightClick()
    {
        return true;
    }

    internal override void RightClick()
    {
        Vector2 direction = Player.instance.transform.position - this.transform.position;

        rigidbody.AddForce(direction.normalized * Time.deltaTime * 1000.0f);
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
