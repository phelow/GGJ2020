using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update

        //vector2 is x/y and we need x/z. 3 people told me to just use vector3 instead.
   float asteroidForce =0.2f;
    Rigidbody2D rb;
    Vector2 v;
    void Awake()
    {
        rb =  gameObject.GetComponent<Rigidbody2D>();
       v = rb.velocity;
        v = v.normalized;
        v += new Vector2(Random.Range(-asteroidForce, asteroidForce),Random.Range(-asteroidForce, asteroidForce));
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BounceAsteroid(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        BounceAsteroid(collision);
    }

    private void BounceAsteroid(Collision2D collision)
    {
        NexusPull nexus = collision.gameObject.GetComponent<NexusPull>();
        if (nexus == null)
        {
            return;
        }

        Vector2 direction = (this.transform.position - nexus.transform.position).normalized;

        rb.AddForce(direction * 10.0f);
    }
}
