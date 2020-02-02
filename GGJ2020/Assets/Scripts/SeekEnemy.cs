using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private bool isTargetingEnemyTeam;
    private float knockbackModifier;

    internal void AssignTarget(GameObject target, float missileKnockbackModifier, bool targetPlayerTeam)
    {
        knockbackModifier = missileKnockbackModifier;
        isTargetingEnemyTeam = targetPlayerTeam;
        StartCoroutine(SeekTarget(target));
        StartCoroutine(DelayedDestroy());
    }
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }

    private IEnumerator SeekTarget(GameObject target)
    {
        for (int fuel = 10; fuel > 0; fuel--)
        {
            if (target == null)
            {
                yield break;
            }

            lock (target)
            {
                // Calculate vector to target
                Vector2 directionToTarget = (target.transform.position - this.transform.position).normalized;

                const float MissileSpeed = 150.0f;

                // Add force in direction
                rigidbody2D.AddForce(directionToTarget * MissileSpeed);
            }

            yield return new WaitForSeconds(1.0f);
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Targetable collidedObject = collision.gameObject.GetComponent<Targetable>();

        if (collidedObject == null)
        {
            return;
        }

        if (!((collidedObject.IsPlayersTeam && isTargetingEnemyTeam) || (!collidedObject.IsPlayersTeam && !isTargetingEnemyTeam)))
        {
            return;
        }

        Vector2 movementVector = (collision.transform.position - this.transform.position).normalized;

        lock (collidedObject)
        {
            collidedObject.Hit(movementVector * 100.0f * knockbackModifier);
        }

        Destroy(this.gameObject);
    }
}
