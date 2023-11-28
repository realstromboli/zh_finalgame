using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1.0f;
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

    public GameObject drop;
    private void OnDestroy()
    {
        Instantiate(drop, transform.position, drop.transform.rotation);
    }
}
