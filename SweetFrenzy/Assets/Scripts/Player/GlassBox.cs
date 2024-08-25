using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBox : MonoBehaviour
{

    [Header("Pick up object from a Glass")]
    [SerializeField] private Player player;

    private Glass glass;
    [SerializeField] private bool hasGlass = false;
    [SerializeField] private bool isTouchingPlayer = false;
    [SerializeField] private bool isTouchingGlass = false;

    void Start()
    {

    }

    void Update()
    {
        CheckGlassCounter();
    }

    private void CheckGlassCounter()
    {
        if (!isTouchingPlayer)
        {
            hasGlass = false;
        }
        else if (isTouchingPlayer && hasGlass && !isTouchingGlass)
        {
            if (player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E))
            {
                glass.GetGlass();
                hasGlass = false;
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return))
            {
                glass.GetGlass();
                hasGlass = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasGlass)
        {
            if (other.gameObject.CompareTag("GlassBox"))
            {
                Glass oneGlass = other.gameObject.GetComponent<Glass>();
                isTouchingPlayer = true;
                hasGlass = true;
                glass = oneGlass;
            }
        }
        if (other.gameObject.CompareTag("Glass"))
        {
            hasGlass = true;
            isTouchingGlass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GlassBox"))
        {
            isTouchingPlayer = false;
        }
        if (other.gameObject.CompareTag("Glass"))
        {
            isTouchingGlass = false;
        }
    }

}