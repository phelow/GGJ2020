using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField]
    private Pullable pullable;
    internal void Hit()
    {
        NexusPull.instance.RemovePullableObject(pullable);
        Destroy(this.gameObject);
    }
}
