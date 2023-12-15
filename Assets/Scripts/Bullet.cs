using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BossEnemyBehavior bossEnemyVariable;
    public PlayerControl playerControlVariable;

    void Start()
    {
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    public float speed = 40.0f;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "BossEnemy")
        {
            bossEnemyVariable = other.gameObject.GetComponent<BossEnemyBehavior>();
            bossEnemyVariable.bossEnemyHealth = bossEnemyVariable.bossEnemyHealth - 1;
            if (bossEnemyVariable.bossEnemyHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
