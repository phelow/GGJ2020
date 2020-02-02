using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalinController : MonoBehaviour
{
    // Start is called before the first frame update
   public float destroyTime = 15;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;

        if (destroyTime <=0)
            gameObject.SetActive(false);

    }
}
