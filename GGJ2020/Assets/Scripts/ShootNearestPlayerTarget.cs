using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNearestPlayerTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject pMissile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootPlayers());
    }

    private IEnumerator ShootPlayers()
    {
        while (true)
        {
            const float MaxTargetingDistance = 5.0f;

            TargetingHelper.ShootNearestTarget(pMissile, this.transform.position, MaxTargetingDistance, true);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
