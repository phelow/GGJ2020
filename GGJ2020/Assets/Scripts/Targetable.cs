using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField]
    private HealthManager health;

    [SerializeField]
    private bool playersTeam;
    internal void Hit()
    {
        health.TakeHit();
    }

    internal bool IsPlayersTeam
    {
        get
        {
            return playersTeam;
        }
    }
}
