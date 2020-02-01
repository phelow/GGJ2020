using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField]
    private GameObject pMissile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootEnemies());
    }

    private IEnumerator ShootEnemies()
    {
        while (true)
        {
            const float MaxTargetingDistance = 15.0f;

            TargetingHelper.ShootNearestTarget(pMissile, this.transform.position, MaxTargetingDistance, false);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
