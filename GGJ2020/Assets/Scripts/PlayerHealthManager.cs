using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField]
    private Player player;

    private float bounce = 0.0f;

    internal override void TakeHit(Vector2 movementVector, float damageToTake = 10.0f)
    {
        bounce += 5.0f;
        player.DrainAllJuice();
        player.MoveInDirection(movementVector * bounce * 200.0f);
    }

    private void Update()
    {
        bounce -= Time.deltaTime;

        bounce = Mathf.Max(0, bounce);
    }
}
