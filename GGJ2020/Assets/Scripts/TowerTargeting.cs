using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [SerializeField]
    private GameObject pMissile;

    [SerializeField]
    protected HealthManager healthManager;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    protected const float MaxTargetingDistance = 35.0f;

    [SerializeField]
    protected float reloadTime = 1.0f;

    [SerializeField]
    float missileKnockBackModifier = 1.0f;
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

    protected virtual IEnumerator ShootEnemies()
    {
        while (true)
        {
            TargetingHelper.ShootNearestTarget(pMissile, this.transform.position, MaxTargetingDistance, missileKnockBackModifier, false);

            yield return new WaitForSeconds(Mathf.Lerp(1.0f, 0.2f, healthManager.GetHealthRatio()) * reloadTime);
        }
    }
}
