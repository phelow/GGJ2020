using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(tower));
    }

    private IEnumerator SpawnRoutine(GameObject spawnable)
    {
        while (true)
        {
            const float DistanceFromNexus = 100.0f;
            // Pick a random point a fixed distance from this spawner
            Vector3 randomPoint = this.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(10.0f, DistanceFromNexus);

            // Create the object at this point.
            GameObject.Instantiate(spawnable, randomPoint, new Quaternion(0, 0, 0, 0), null);
            yield return new WaitForSeconds(10.0f);
        }
    }
}
