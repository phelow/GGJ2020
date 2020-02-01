using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NexusPull : MonoBehaviour
{
    internal static NexusPull instance;

    HashSet<Pullable> pullableSpaceObjects = new HashSet<Pullable>();
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(PullObjectsOnPulse());
    }

    private IEnumerator PullObjectsOnPulse()
    {
        while (true)
        {
            lock (pullableSpaceObjects)
            {
                foreach (Pullable pullable in pullableSpaceObjects)
                {
                    pullable.PullTowardsNexus(this.transform.position);
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }


    internal void AddPullableObject(Pullable pullable)
    {
        lock (pullableSpaceObjects)
        {
            pullableSpaceObjects.Add(pullable);

        }
    }

    internal void RemovePullableObject(Pullable pullable)
    {
        lock (pullableSpaceObjects)
        {
            pullableSpaceObjects.Remove(pullable);

        }
    }
}
