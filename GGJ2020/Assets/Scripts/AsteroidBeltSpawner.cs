using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltSpawner : MonoBehaviour
{
    public int asteroidAmount = 500;
    public int radius = 50;
    public GameObject asteroidObj;
    Vector3 CenterPos;

    // Start is called before the first frame update
    void Start()
    {
        CenterPos = transform.position;
        GameObject curAsteroid;
        GameObject lastAsteroid = null;
        for (int i = 0; i <= asteroidAmount; i++)
        {
            Vector3 ringVector = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
            ringVector.Normalize();

            //multiply it with a random value to point to a specific location
            // 1st parameter makes it so that asteroids dont spawn in middle.
            ringVector *= (Random.Range(10.0f, radius));
            curAsteroid = GameObject.Instantiate(asteroidObj, CenterPos + ringVector, Quaternion.identity,this.transform);
            float circularScale = Random.Range(5f, 10.0f);
            curAsteroid.transform.localScale = new Vector3(circularScale, circularScale, Random.Range(5f, 10.0f)).normalized * Random.Range(2.0f, 20.0f);

            if (lastAsteroid != null)
            {
                lastAsteroid.GetComponent<SpringJoint2D>().connectedBody = curAsteroid.GetComponent<Rigidbody2D>();
                lastAsteroid.GetComponent<SpringJoint2D>().autoConfigureDistance = false;
                lastAsteroid.GetComponent<SpringJoint2D>().distance += lastAsteroid.transform.localScale.magnitude +  curAsteroid.transform.localScale.magnitude + 10.0f;
            }

            lastAsteroid = curAsteroid;
        }
    }
}
