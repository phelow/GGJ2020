using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public void PickupResource()
    {
        ResourceTracker.instance.AddResource();
        Destroy(this.gameObject);
    }
}
