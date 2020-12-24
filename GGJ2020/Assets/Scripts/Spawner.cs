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
        float timeToSpawn = 5.5f;
        float radians = 0.0f;
        while (true)
        {
            float interval = timeToSpawn + .25f;
            const float DistanceFromNexus = 100.0f;
            // Pick a random point a fixed distance from this spawner
            Vector3 randomPoint = this.transform.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)).normalized * DistanceFromNexus;

            // Choose an object to spawn
            GameObject objectToSpawn = SpawnableObjects[Random.Range(0, SpawnableObjects.Count)];

            // Create the object at this point.
            objectToSpawn.layer = LayerMask.NameToLayer("Enemies");
            GameObject.Instantiate(objectToSpawn, randomPoint, new Quaternion(0, 0, 0, 0), null);
            yield return new WaitForSeconds(interval);
            timeToSpawn *= .98f;
            radians += interval/10.0f;
        }
    }
}
