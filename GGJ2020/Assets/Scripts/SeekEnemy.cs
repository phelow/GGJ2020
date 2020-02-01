using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    internal void AssignTarget(GameObject target)
    {
        StartCoroutine(SeekTarget(target));
    }

    private IEnumerator SeekTarget(GameObject target)
    {
        for (int fuel = 5; fuel > 0; fuel--)
        {
            // Calculate vector to target
            Vector2 directionToTarget = (target.transform.position - this.transform.position).normalized;

            const float MissileSpeed = 150.0f;

            // Add force in direction
            rigidbody2D.AddForce(directionToTarget * MissileSpeed);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
