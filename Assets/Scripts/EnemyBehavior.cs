using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    public PlayerControl playerControlVariable;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    
    void Update()
    {
        enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
    }

    //doesn't increase speed for some reason
    public void increaseDifficulty()
    {
        if (playerControlVariable.score > 10)
        {
            speed = 10;
        }
        if (playerControlVariable.score > 20)
        {
            speed = 20;
        }
    }
}
