using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltSpawner : MonoBehaviour
{
    public int asteroidAmount = 500;
    public int radius = 50;
    public GameObject asteroidObj;
    Vector3 CenterPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GameObject curAsteroid;
        for (int i = 0; i <= asteroidAmount; i++)
        {
            Vector3 ringVector = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            ringVector.Normalize();
            //multiply it with a random value to point to a specific location
            // 1st parameter makes it so that asteroids dont spawn in middle.
            ringVector *= (Random.Range(radius / 1.1f, radius));
            curAsteroid = GameObject.Instantiate(asteroidObj, CenterPos + ringVector, Quaternion.identity);
            curAsteroid.transform.localScale = new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
            curAsteroid.transform.parent = this.gameObject.transform;
        }
    }
}
