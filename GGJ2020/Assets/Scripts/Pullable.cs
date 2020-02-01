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

    internal void PullTowardsNexus(Vector3 nexusPosition)
    {
        // Calculate distance to nexus
        float distance = Vector2.Distance(this.transform.position, nexusPosition);

        const float MaxForce = 5.0f;
        const float MinForce = .1f;

        const float MaxDistance = 20.0f;
        const float MinDistance = 1.0f;

        const float DistanceExponent = 5.0f;
        // Calculate force to inflict
        float force = Mathf.Lerp(MaxForce, MinForce, Mathf.InverseLerp(MinDistance, MaxDistance, Mathf.Pow(distance, DistanceExponent)));

        // Calculate direction to pull object in
        Vector2 direction = (nexusPosition - this.transform.position).normalized;

        // Multiply force by direction
        Vector2 multipliedDirection = direction * force;

        // Add force to gameobject
        rigidbody2D.AddForce(multipliedDirection);
    }
}
