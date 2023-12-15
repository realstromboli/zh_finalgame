using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public PlayerControl playerControlVariable;

    void Start()
    {
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    
    void Update()
    {
        
    }
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public float spreadAngle = 15;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
    }

    public void CallFire()
    {
        Fire();
        
        if (playerControlVariable.score >= 30)
        {
            transform.Rotate(Vector3.up, spreadAngle);
            Fire();
            transform.Rotate(Vector3.up, -spreadAngle * 2);
            Fire();
            transform.Rotate(Vector3.up, spreadAngle);
        }
    }
}
