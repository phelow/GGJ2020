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
        float distance = Vector3.Distance(this.transform.position, nexusPosition);

        const float MaxForce = 10;
        const float MinForce = 1;

        const float MaxDistance = 100.0f;
        const float MinDistance = 1.0f;

        const float DistanceExponent = 3.0f;
        // Calculate force to inflict
        float force = Mathf.Lerp(MaxForce, MinForce, Mathf.InverseLerp(MinDistance, MaxDistance, Mathf.Pow(distance, DistanceExponent)));

        // Calculate direction to pull object in
        Vector3 direction = (nexusPosition - this.transform.position).normalized;

        // Multiply force by direction
        Vector3 multipliedDirection = direction * force;

        // Add force to gameobject
        rigidbody2D.AddForce(multipliedDirection);
    }
}
