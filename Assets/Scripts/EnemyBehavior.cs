using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed;
    private Rigidbody enemyRb;
    private GameObject player;
    public PlayerControl playerControlVariable;
    public Timer timerVariable;

    private AudioSource enemyAudio;
    public AudioClip gunSound;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
        timerVariable = GameObject.Find("Timer").GetComponent<Timer>();
        enemyAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        enemyRb.AddForce((player.transform.position - transform.position).normalized * enemySpeed);
        increaseDifficulty();
        stayInboundsEnemy();
    }

    //doesn't increase speed for some reason
    public void increaseDifficulty()
    {
        if (playerControlVariable.score > 10)
        {
            enemySpeed = 6;
        }
        if (playerControlVariable.score > 20)
        {
            enemySpeed = 7;
        }
        if (playerControlVariable.score > 30)
        {
            enemySpeed = 8;
        }
        if (playerControlVariable.score > 40)
        {
            enemySpeed = 9;
        }
        if (playerControlVariable.score > 50)
        {
            enemySpeed = 10;
        }
        if (playerControlVariable.score > 60)
        {
            enemySpeed = 20;
        }
        if (playerControlVariable.score > 70)
        {
            enemySpeed = 30;
        }
        if (playerControlVariable.score > 80)
        {
            enemySpeed = 40;
        }
        if (playerControlVariable.score > 90)
        {
            enemySpeed = 50;
        }
        if (playerControlVariable.score > 100)
        {
            enemySpeed = 100;
        }
    }

    public float xRangeEnemy;
    public float zRangeEnemy;

    public void stayInboundsEnemy()
    {
        if (transform.position.x < -xRangeEnemy)
        {
            transform.position = new Vector3(-xRangeEnemy, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRangeEnemy)
        {
            transform.position = new Vector3(xRangeEnemy, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -zRangeEnemy)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRangeEnemy);
        }
        if (transform.position.z > zRangeEnemy)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeEnemy);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            Destroy(gameObject);
            timerVariable.remainingDuration = timerVariable.remainingDuration - 2;
        }

    }
}
