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

    [Header("Movement settings")]
    [SerializeField] private float speed = 5f;                  // Velocidad de movimiento 
    [SerializeField] private float normalSpeed = 5f;            // Velocidad normal
    [SerializeField] private float rotationSpeed = 5f;          // Velocidad de rotacion

    [Header("Sprint settings")]
    [SerializeField] private float sprintSpeed = 10f;           // Velocidad de sprint
    [SerializeField] private float sprintDuration = 3f;         // Duración del sprint en segundos
    [SerializeField] private float sprintCooldown = 5f;         // Tiempo de espera entre sprints en segundos
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private bool canSprint = true;
    [SerializeField] private float sprintTimer = 0f;            // Temporizador del sprintDuration
    [SerializeField] private float sprintCooldownTimer = 0f;    // Temporizador del sprintCooldown

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Movement();
        Sprint();
        UpdateSprint();
    }

    #region Movement and rotation

    /// <summary>
    /// Player's walk movement
    /// </summary>
    private void Movement()
    {
        float horizontalInput;
        float verticalInput;

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

    /// <summary>
    /// Player's rotation based on direction of movement
    /// </summary>
    /// <param name="movementDirection"></param>
    private void Rotation(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region Sprint

    /// <summary>
    /// Sprint functionality
    /// </summary>
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

    /// <summary>
    /// Update sprint settings
    /// </summary>
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
}
