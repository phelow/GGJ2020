using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A gameobject that is pulled towards the nexus.
/// </summary>
public class Pullable : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        NexusPull.instance.AddPullableObject(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual float GetPullForce(float distance)
    {
        const float MaxForce = 20.0f;
        const float MinForce = .5f;

        const float DistanceExponent = 2.0f;
        return Mathf.Lerp(MaxForce, MinForce, Mathf.Pow( distance,2.0f)) * 100.0f;
    }

    internal void PullTowardsNexus(Vector3 nexusPosition)
    {
        // Calculate distance to nexus
        float distance = Vector2.Distance(this.transform.position, nexusPosition);

        // Calculate force to inflict
        float force = GetPullForce(distance);

        // Calculate direction to pull object in
        Vector2 direction = (nexusPosition - this.transform.position).normalized;

        // Multiply force by direction
        Vector2 multipliedDirection = direction * force;

        // Add force to gameobject
        rigidbody2D.AddForce(multipliedDirection * rigidbody2D.mass);
    }
}
