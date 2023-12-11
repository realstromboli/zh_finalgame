using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;
    [SerializeField] private bool isGamepad;
    private CharacterController controller;
    private Vector2 movement;
    private Vector2 aim;
    private Vector3 velocity;
    public float horizontalInput;
    public float verticalInput;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    public Timer timerVariable;
    public GameManager gameManagerVariable;

    public bool cantFire;
    public float fireCooldown;

    public GameObject powerupIndicator;
    private AudioSource playerAudio;
    public AudioClip gunSound;
    public AudioClip pickupSound;
    public AudioClip sparkSound;

    //old, dont wan't to remove or deactivate tho. scared to lol
    public Rigidbody2D rb;
    public Weapon weapon;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        myText.text = "";
        gameManagerVariable = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (gameManagerVariable.isGameActive == true)
        {
            HandleInput();
            HandleMovement();
            HandleRotation();
            if (Input.GetMouseButtonDown(0) && !cantFire)
            {
                playerAudio.PlayOneShot(gunSound, 1.0f);
                weapon.Fire();
                cantFire = true;
                StartCoroutine(FireCooldown());
            }
        }
        
        stayInbounds();
        updateScore();
    }

    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        cantFire = false;
    }

    public void Move()
    {
        //input for top down thing, moving LR on X and UpDown on Z
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * speed);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            //rotates player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newrotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    private void LookAt(Vector3 LookPoint)
    {
        Vector3 heightcorrectedPoint = new Vector3(LookPoint.x, transform.position.y, LookPoint.z);
        transform.LookAt(heightcorrectedPoint);
    }

    public void OnDeviceChange (PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    public float xRange;
    public float zRange;

    public void stayInbounds() //self explanatory
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    public Text myText;
    public int score;
    public Text scoreText;
    public void printScore()
    {
        Debug.Log(score);
    }

    public bool hasSpamPower;
    public float powerupDuration;
    public EnemyBehavior enemyVariable;

    public void OnTriggerEnter(Collider collider)
    {
        score = score + 1;
        if (collider.tag == "PickupToCollect")
        {
            playerAudio.PlayOneShot(pickupSound, 1.0f);
            Destroy(collider.gameObject);
            timerVariable.Being(10);
        }
        if (collider.tag == "SpamPowerup")
        {
            playerAudio.PlayOneShot(sparkSound, 1.0f);
            hasSpamPower = true;
            Destroy(collider.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    IEnumerator PowerupCooldown()
    {
        fireCooldown = 0.1f;
        speed = 25;
        yield return new WaitForSeconds(powerupDuration);
        hasSpamPower = false;
        fireCooldown = 0.4f;
        speed = 12;
        powerupIndicator.gameObject.SetActive(false);
    }

    public void updateScore()
    {
        myText.text = "" + score;
    }

    
}

