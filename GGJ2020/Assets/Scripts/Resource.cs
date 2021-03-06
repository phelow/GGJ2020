﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }

    public void PickupResource()
    {
        ResourceTracker.instance.AddResource();
        Sickle.sickle.SetTerminus(this.transform.position);
        Destroy(this.gameObject);
    }
}
