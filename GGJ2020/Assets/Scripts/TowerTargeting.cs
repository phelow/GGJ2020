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

            // Find the nearest enemy in range
            Collider2D[] collisions = Physics2D.OverlapCircleAll(this.transform.position, MaxTargetingDistance);

            Targetable closestTarget = null;
            foreach (Collider2D collision in collisions)
            {
                Targetable target = collision.GetComponentInChildren<Targetable>();

                if (target == null)
                {
                    continue;
                }

                if (closestTarget == null)
                {
                    closestTarget = target;
                    continue;
                }

                float distance = Vector2.Distance(target.transform.position, this.transform.position);
                float minDistance = Vector2.Distance(closestTarget.transform.position, this.transform.position);
                if (distance < minDistance)
                {
                    closestTarget = target;
                }
            }

            if (closestTarget != null)
            {
                // Shoot at it
                SeekEnemy missile = GameObject.Instantiate(pMissile, transform.position, new Quaternion(0, 0, 0, 0)).GetComponent<SeekEnemy>();

                missile.AssignTarget(closestTarget.gameObject);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
