using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    [SerializeField]
    private GameObject prefabResource;
    protected override void OnKilled()
    {
        GameObject.Instantiate(prefabResource, this.transform.position, new Quaternion(0, 0, 0, 0), null);
    }
}
