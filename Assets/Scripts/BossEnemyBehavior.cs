using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour
{
    public float bossEnemySpeed;
    private Rigidbody bossEnemyRb;
    private GameObject player;
    public PlayerControl playerControlVariable;
    public Timer timerVariable;
    public int bossEnemyHealth = 5;

    void Start()
    {
        bossEnemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
        timerVariable = GameObject.Find("Timer").GetComponent<Timer>();
    }


    void Update()
    {
        transform.Translate((player.transform.position - transform.position).normalized * bossEnemySpeed * Time.deltaTime);
        //enemyRb.AddForce((player.transform.position - transform.position).normalized * enemySpeed);
        increaseDifficulty();
        stayInboundsBossEnemy();
    }


    public void increaseDifficulty()
    {
        if (playerControlVariable.score > 10)
        {
            bossEnemySpeed = 6.5f;
        }
        if (playerControlVariable.score > 20)
        {
            bossEnemySpeed = 8;
        }
        if (playerControlVariable.score > 30)
        {
            bossEnemySpeed = 9.5f;
        }
        if (playerControlVariable.score > 40)
        {
            bossEnemySpeed = 11;
        }
        if (playerControlVariable.score > 50)
        {
            bossEnemySpeed = 12.5f;
        }
        if (playerControlVariable.score > 60)
        {
            bossEnemySpeed = 14;
        }
        if (playerControlVariable.score > 70)
        {
            bossEnemySpeed = 15.5f;
        }
        if (playerControlVariable.score > 80)
        {
            bossEnemySpeed = 17;
        }
        if (playerControlVariable.score > 90)
        {
            bossEnemySpeed = 25;
        }
        if (playerControlVariable.score > 100)
        {
            bossEnemySpeed = 50;
        }
    }

    public float xRangeEnemy;
    public float zRangeEnemy;

    public void stayInboundsBossEnemy()
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
            timerVariable.remainingDuration = timerVariable.remainingDuration - 5;
        }

    }
}
