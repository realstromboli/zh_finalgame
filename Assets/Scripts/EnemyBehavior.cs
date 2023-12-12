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

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
        timerVariable = GameObject.Find("Timer").GetComponent<Timer>();
    }

    
    void Update()
    {
        transform.Translate((player.transform.position - transform.position).normalized * enemySpeed * Time.deltaTime);
        //enemyRb.AddForce((player.transform.position - transform.position).normalized * enemySpeed);
        increaseDifficulty();
        stayInboundsEnemy();
    }

    //doesn't increase speed for some reason
    public void increaseDifficulty()
    {
        if (playerControlVariable.score > 10)
        {
            enemySpeed = 7.5f;
        }
        if (playerControlVariable.score > 20)
        {
            enemySpeed = 10;
        }
        if (playerControlVariable.score > 30)
        {
            enemySpeed = 12.5f;
        }
        if (playerControlVariable.score > 40)
        {
            enemySpeed = 15;
        }
        if (playerControlVariable.score > 50)
        {
            enemySpeed = 17.5f;
        }
        if (playerControlVariable.score > 60)
        {
            enemySpeed = 20;
        }
        if (playerControlVariable.score > 70)
        {
            enemySpeed = 22.5f;
        }
        if (playerControlVariable.score > 80)
        {
            enemySpeed = 25;
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
