using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField]
    private Player player;

    internal override void TakeHit(Vector2 movementVector, float damageToTake = 10.0f)
    {
        //player.DrainAllJuice();
        player.MoveInDirection(movementVector * 10.0f);
    }
}
