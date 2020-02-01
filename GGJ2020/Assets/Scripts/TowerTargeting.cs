using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField]
    private GameObject pMissile;

    [SerializeField]
    private HealthManager healthManager;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootEnemies());
        StartCoroutine(Drift());
    }

    private IEnumerator Drift()
    {
        while (true)
        {
            Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;

            const float MaxForce = 10.0f;
            const float MinForce = 0.0f;
            float force = Mathf.Lerp(MinForce, MaxForce, healthManager.GetHealthRatio());
            rigidbody2D.AddForce(randomDirection * force);

            yield return new WaitForSeconds(Mathf.Lerp(0.1f, 1.0f, healthManager.GetHealthRatio()));
        }
    }

    private IEnumerator ShootEnemies()
    {
        while (true)
        {
            const float MaxTargetingDistance = 15.0f;

            TargetingHelper.ShootNearestTarget(pMissile, this.transform.position, MaxTargetingDistance, false);

            yield return new WaitForSeconds(Mathf.Lerp(10.0f, 1.0f, healthManager.GetHealthRatio()));
        }
    }
}
