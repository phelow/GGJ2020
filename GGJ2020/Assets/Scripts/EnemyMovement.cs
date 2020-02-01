using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    HealthManager enemyHealthManager;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NexusPull nexus = collision.gameObject.GetComponent<NexusPull>();
        if (nexus == null)
        {
            return;
        }

        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health == null)
        {
            return;
        }

        health.TakeHit(10.0f);
        enemyHealthManager.TakeHit(float.MaxValue);
    }
}
