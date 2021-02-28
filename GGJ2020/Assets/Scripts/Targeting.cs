using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetingHelper
{
    internal static void ShootNearestTarget(GameObject missilePrefab, Vector2 shooterPosition, float MaxTargetingDistance, float missileKnockbackModifier, bool targetPlayerTeam)
    {
        // Find the nearest enemy in range
        Collider2D[] collisions = Physics2D.OverlapCircleAll(shooterPosition, MaxTargetingDistance);

        Targetable closestTarget = null;
        foreach (Collider2D collision in collisions)
        {
            Targetable target = collision.GetComponentInChildren<Targetable>();

            if (target == null)
            {
                continue;
            }

            NexusPull nexus = collision.GetComponent<NexusPull>();

            if (nexus != null)
            {
                continue;
            }

            if (target.IsPlayersTeam && !targetPlayerTeam)
            {
                continue;
            }

            if (!target.IsPlayersTeam && targetPlayerTeam)
            {
                continue;
            }
            
            Vector2 direction = (new Vector2(target.transform.position.x, target.transform.position.y) - shooterPosition).normalized;


            float distance = Vector2.Distance(target.transform.position, shooterPosition);

            float minDistance = Mathf.Infinity;

            if (closestTarget != null)
            {
                minDistance = Vector2.Distance(closestTarget.transform.position, shooterPosition);
            }
            
                RaycastHit2D[] hit = AreThereObstacles(shooterPosition, direction, distance);
                if (hit.Length != 0)
                {
                    continue;
                }

            if (distance < minDistance)
            {
                closestTarget = target;
            }
        }

        if (closestTarget != null)
        {
            // Shoot at it
            SeekEnemy missile = GameObject.Instantiate(missilePrefab, shooterPosition, new Quaternion(0, 0, 0, 0)).GetComponent<SeekEnemy>();

            missile.AssignTarget(closestTarget.gameObject, missileKnockbackModifier, targetPlayerTeam);
        }
    }

    internal static void FireLasersAtNearestTarget(GameObject pLaser, Vector3 shooterPosition, float MaxTargetingDistance, bool targetPlayerTeam)
    {
        // Find the nearest enemy in range
        Collider2D[] collisions = Physics2D.OverlapCircleAll(shooterPosition, MaxTargetingDistance);

        Targetable closestTarget = null;
        foreach (Collider2D collision in collisions)
        {
            Targetable target = collision.GetComponentInChildren<Targetable>();

            if (target == null)
            {
                continue;
            }

            NexusPull nexus = collision.GetComponent<NexusPull>();

            if (nexus != null)
            {
                continue;
            }

            if (target.IsPlayersTeam && !targetPlayerTeam)
            {
                continue;
            }

            if (!target.IsPlayersTeam && targetPlayerTeam)
            {
                continue;
            }

            Vector2 direction = (target.transform.position - shooterPosition).normalized;


            float distance = Vector2.Distance(target.transform.position, shooterPosition);

            float minDistance = Mathf.Infinity;

            if (closestTarget != null)
            {
                minDistance = Vector2.Distance(closestTarget.transform.position, shooterPosition);
            }

            if (distance < minDistance)
            {
                closestTarget = target;
            }
        }

        if (closestTarget != null)
        {
            // Shoot at it
            SeekEnemy missile = GameObject.Instantiate(pLaser, shooterPosition, new Quaternion(0, 0, 0, 0)).GetComponent<SeekEnemy>();

missile.AssignTarget(closestTarget.gameObject, 0, targetPlayerTeam);
        }
    }

    private static RaycastHit2D[] AreThereObstacles(Vector3 startingPoint, Vector2 direction, float distance)
    {
        float modifiedDistance = distance + 4.0f;
        RaycastHit2D[] hit = Physics2D.RaycastAll(startingPoint, direction, modifiedDistance, LayerMask.GetMask("Default"));
        Debug.DrawRay(startingPoint, direction * modifiedDistance);
        return hit;
    }
}
