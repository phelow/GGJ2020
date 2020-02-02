using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    internal override void TakeHit(float damageToTake = 10.0f)
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
