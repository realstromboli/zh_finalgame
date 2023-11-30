using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
    }

}
