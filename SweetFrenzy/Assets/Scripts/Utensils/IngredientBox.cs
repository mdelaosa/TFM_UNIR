using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{

    [Header("Pick up object from a box")]
    [SerializeField] private GameObject counter;

    [SerializeField] public GameObject ingredients;
    [SerializeField] private bool hasIngredient = false;
    [SerializeField] private bool isTouchingPlayer = false;
    [SerializeField] private bool isTouchingFruit = false;

    void Start()
    {

    }

    void Update()
    {
        checkFruitCounter();
    }

    private void ingredient()
    {
        if (hasIngredient)
        {
            Instantiate(ingredients, transform.position, Quaternion.identity);
        }
    }

    private void checkFruitCounter()
    {
        if (!isTouchingPlayer)
        {
            hasIngredient = false;
        }
        else if (isTouchingPlayer && hasIngredient && Input.GetKeyDown(KeyCode.Space))
        {
            hasIngredient = true;
            ingredient();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasIngredient)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isTouchingPlayer = true;
                hasIngredient = true;
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
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
        if (other.gameObject.CompareTag("Food"))
        {
            isTouchingFruit = false;
        }
    }

}