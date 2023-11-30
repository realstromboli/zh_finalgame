using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public GameObject player;
    private Vector3 offset = new Vector3(0, 20, 0);

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
