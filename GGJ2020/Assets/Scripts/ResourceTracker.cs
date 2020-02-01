using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    internal static ResourceTracker instance;

    [SerializeField]
    private ProgressBarPro ResourceBar;

    private int chargesCollected = 0;
    private int MaxCharges = 5;

    private void Awake()
    {
        instance = this;
    }

    internal void AddResource()
    {
        chargesCollected++;
        ResourceBar.SetValue(chargesCollected * 1.0f / MaxCharges);
    }
}
