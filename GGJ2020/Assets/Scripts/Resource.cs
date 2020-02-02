using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    private bool ShouldDestroy = true;
    private void Awake()
    {
        if (ShouldDestroy)
        {
            StartCoroutine(DelayedDestroy());
        }
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
