using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField]
    private HealthManager health;

    [SerializeField]
    private bool playersTeam;
    internal void Hit(Vector2 movementVector)
    {
        health.TakeHit(movementVector);
    }

    internal bool IsPlayersTeam
    {
        get
        {
            return playersTeam;
        }
    }
}
