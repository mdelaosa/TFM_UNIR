using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlBox : MonoBehaviour
{

    [Header("Pick up object from a Bowl")]
    [SerializeField] private Player player;

    private Bowl bowls;
    [SerializeField] private bool hasBowl = false;
    [SerializeField] private bool isTouchingPlayer = false;
    [SerializeField] private bool isTouchingBowl = false;

    void Start()
    {

    }

    void Update()
    {
        CheckBowlCounter();
    }

    private void CheckBowlCounter()
    {
        if (!isTouchingPlayer)
        {
            hasBowl = false;
        }
        else if (isTouchingPlayer && hasBowl && !isTouchingBowl)
        {
            if(player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E))
            {
                bowls.GetBowl();
                hasBowl = false;
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return))
            {
                bowls.GetBowl();
                hasBowl = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBowl)
        {
            if (other.gameObject.CompareTag("BowlBox"))
            {
                Bowl oneBowl = other.gameObject.GetComponent<Bowl>();
                isTouchingPlayer = true;
                hasBowl = true;
                bowls = oneBowl;
            }
        }
        if (other.gameObject.CompareTag("Bowl"))
        {
            hasBowl = true;
            isTouchingBowl = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BowlBox"))
        {
            isTouchingPlayer = false;
        }
        if (other.gameObject.CompareTag("Bowl"))
        {
            isTouchingBowl = false;
        }
    }

}