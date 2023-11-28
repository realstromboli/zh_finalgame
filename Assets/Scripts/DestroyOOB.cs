using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private float zRange = 30.0f;
    private float xRange = 30.0f;
    void Update()
    {
        if (transform.position.z > zRange)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < -zRange)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > xRange)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -xRange)
        {
            Destroy(gameObject);
        }
    }
}
