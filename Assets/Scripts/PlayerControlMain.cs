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
    [SerializeField] private float speed = 5f;
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

    //old, dont wan't to remove or deactivate tho. scared to lol
    public Rigidbody2D rb;
    public Weapon weapon;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        myText.text = "";
        // wanted to put score on screen at the end of 60 seconds but couldnt figure it out
        //Invoke(printScore, 30f);
    }

    
    void Update()
    {
        //Move();
        HandleInput();
        HandleMovement();
        HandleRotation();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Fire();
        }
        stayInbounds();
        updateScore();
    }

    public void Move()
    {
        //input for top down thing, moving LR on X and UpDown on Z
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        

        // below is code from a top down twin stick tutorial but it was in 2D so the atan2 and ScreenToWorldPoint stuff didn't work correctly

        //moveDirection = new Vector3(horizontalInput, verticalInput).normalized;
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        //void dontinclude
        //{
        //    rb.velocity = new Vector3(moveDirection.x * speed, moveDirection.z * speed);

        //    Vector3 aimDirection = mousePosition - rb.position;
        //    float aimAngle = Mathf.Atan2(aimDirection.z, aimDirection.x) * Mathf.Rad2Deg - 90f;
        //    rb.rotation = aimAngle;
        //}
    

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

    public float xRange = 15.0f;
    public float zRange = 15.0f;

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
    private void OnTriggerEnter(Collider collider)
    {
        score = score + 1;
        if (collider.tag == "PickupToCollect")
        {
            Destroy(collider.gameObject);
        }
    }
    public void updateScore()
    {
        myText.text = "" + score;
    }

}

