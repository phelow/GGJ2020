using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : TowerTargeting
{
    [SerializeField]
    private GameObject pLaser;
    protected override IEnumerator ShootEnemies()
    {
        while (true)
        {
            TargetingHelper.FireLasersAtNearestTarget(pLaser, this.transform.position, MaxTargetingDistance, false);

            yield return new WaitForSeconds(Mathf.Lerp(5.0f, 1.0f, healthManager.GetHealthRatio()) * reloadTime);
        }
    }
}
