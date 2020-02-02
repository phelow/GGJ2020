using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    [SerializeField]
    internal static Hammer instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
