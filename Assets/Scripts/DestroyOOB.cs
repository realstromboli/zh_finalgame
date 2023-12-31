using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private float zRange = 60.0f;
    private float xRange = 60.0f;
    private float yRange = 5.0f;
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
        if (transform.position.y > yRange)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -yRange)
        {
            Destroy(gameObject);
        }
    }
}
