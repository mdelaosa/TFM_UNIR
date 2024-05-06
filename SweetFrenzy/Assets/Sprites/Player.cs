using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerID options
/// </summary>
public enum PlayerID
{
    player1 = 1,
    player2 = 2
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Player's movement
    /// </summary>
    private void Move()
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

        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
        
        Rotate(movementDirection);
    }

    /// <summary>
    /// Player's rotation based on direction of movement
    /// </summary>
    /// <param name="movementDirection"></param>
    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime);
        }
    }
}
