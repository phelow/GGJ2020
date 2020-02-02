using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateRandomlyOverTime());
    }

    private IEnumerator RotateRandomlyOverTime()
    {
        Vector3 rotationVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(10.0f,50f);
        while (true)
        {
            transform.Rotate(Time.deltaTime * rotationVector);
            yield return new WaitForEndOfFrame();
        }
    }
}
