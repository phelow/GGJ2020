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
    void Start()
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
}
