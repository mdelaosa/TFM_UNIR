using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    player1 = 1,
    player2 = 2
}

public class Player : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private PlayerID playerID;
    [SerializeField] private bool isMoving = false;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    [SerializeField] private float forceMovement;

    [Header("Movement settings")]
    [SerializeField] private float speed = 5f;                   
    [SerializeField] private float normalSpeed = 5f;            
    [SerializeField] private float rotationSpeed = 5f;          

    [Header("Sprint settings")]
    [SerializeField] private float sprintSpeed = 10f;          
    [SerializeField] private float sprintDuration = 3f;         
    [SerializeField] private float sprintCooldown = 5f;         
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private float sprintTimer = 0f;            
    [SerializeField] private float sprintCooldownTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        UpdateAnimation();
    }

    private void Move()
    {
        Movement();
        Sprint();
        UpdateSprint();
    }

    #region Movement and rotation
    private void Movement()
    {

        if (playerID == PlayerID.player1)
        {
            horizontalInput = Input.GetAxis("Horizontal_P1");
            verticalInput = Input.GetAxis("Vertical_P1");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal_P2");
            verticalInput = Input.GetAxis("Vertical_P2");
        }


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        transform.position = transform.position + movementDirection * (isSprinting ? sprintSpeed : speed) * Time.deltaTime;

        Rotation(movementDirection);
    }

    private void Rotation(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(horizontalInput, 0, verticalInput).normalized * forceMovement, ForceMode.Force);
        LimitMovement();
    }

    private void LimitMovement()
    {
        Vector3 planeMovement = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 limitMovement = Vector3.ClampMagnitude(planeMovement, speed);
        rb.velocity = new Vector3(limitMovement.x, rb.velocity.y, limitMovement.z);

    }

    #endregion

    #region Sprint
    private void Sprint()
    {
        bool sprintInput;
        if (playerID == PlayerID.player1)
        {
            sprintInput = Input.GetKeyDown(KeyCode.LeftShift);
        }
        else
        {
            sprintInput = Input.GetKeyDown(KeyCode.RightShift);
        }

        if (sprintInput)
        {
            if (!isSprinting && canSprint)
            {
                isSprinting = true;
                canSprint = false;
                speed = sprintSpeed;
                sprintTimer = sprintDuration;
            }
        }        
    }

    private void UpdateSprint()
    {
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            
            if (sprintTimer <= 0f)
            {
                isSprinting = false;
                sprintCooldownTimer = sprintCooldown;
                speed = normalSpeed;
            }
        }
        else
        {
            if (sprintCooldownTimer <= 0f)
            {
                canSprint = true;
            }
            else
            {
                sprintCooldownTimer -= Time.deltaTime;
                canSprint = false;
            } 
        }
    }

    #endregion

    #region Getters and setters
    public PlayerID GetPlayerID()
    {
        return playerID;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    #endregion

    #region Animation
    private void UpdateAnimation()
    {
        if (isMoving)
        {
            gameObject.GetComponent<Animator>().enabled = true;

            gameObject.GetComponent<Animator>().Play("Caminar");

        }
        else if (!isMoving)
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }

    }
    #endregion
}
