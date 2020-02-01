using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> SpawnableObjects;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(SpawnableObjects));
    }

    private IEnumerator SpawnRoutine(List<GameObject> spawnables)
    {
        while (true)
        {
            const float DistanceFromNexus = 100.0f;
            // Pick a random point a fixed distance from this spawner
            Vector3 randomPoint = this.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * DistanceFromNexus;

            // Choose an object to spawn
            GameObject objectToSpawn = SpawnableObjects[Random.Range(0, SpawnableObjects.Count)];

            // Create the object at this point.
            GameObject.Instantiate(objectToSpawn, randomPoint, new Quaternion(0, 0, 0, 0), null);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
