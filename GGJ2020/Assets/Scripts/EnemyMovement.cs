using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wander());
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            // Determine the current velocity vector and normalize
            Vector2 currentDirection = rigidbody2D.velocity.normalized;

            // Generate a random deviation
            Vector2 deviation = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

            Vector2 newImpulse = currentDirection + deviation;

            float WanderIntensity = 1.0f;

            // Multiply deviation by intensity
            newImpulse *= WanderIntensity;

            // Add the deviation to the current movement
            rigidbody2D.AddForce(newImpulse);

            yield return new WaitForSeconds(1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
