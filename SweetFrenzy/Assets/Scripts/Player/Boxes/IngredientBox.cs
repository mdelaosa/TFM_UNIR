using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{

    [Header("Pick up object from a box")]
    [SerializeField] private Player player;

    private Ingredient ingredients;
    [SerializeField] private bool hasIngredient = false;
    [SerializeField] private bool isTouchingPlayer = false;
    [SerializeField] private bool isTouchingFruit = false;

    void Start()
    {

    }

    void Update()
    {
        CheckFruitCounter();
    }

    private void CheckFruitCounter()
    {
        if (!isTouchingPlayer)
        {
            hasIngredient = false;
            isTouchingFruit = false;
        }
        else if (isTouchingPlayer && hasIngredient && !isTouchingFruit)
        {
            if(player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E))
            {
                ingredients.GetIngredient();
                hasIngredient = false;
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return))
            {
                ingredients.GetIngredient();
                hasIngredient = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasIngredient)
        {
            if (other.gameObject.CompareTag("FoodBox"))
            {
                Ingredient food = other.gameObject.GetComponent<Ingredient>();
                isTouchingPlayer = true;
                hasIngredient = true;
                ingredients = food;
            }
        }
        if (other.gameObject.CompareTag("Food"))
        {
            hasIngredient = true;
            isTouchingFruit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FoodBox"))
        {
            isTouchingPlayer = false;
        }
        if (other.gameObject.CompareTag("Food"))
        {
            isTouchingFruit = false;
            hasIngredient = false;
            isTouchingFruit = false;
        }
    }

}