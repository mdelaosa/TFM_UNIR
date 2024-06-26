using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{

    [Header("Pick up object from a box")]
    [SerializeField] private GameObject counter;

    [SerializeField] public GameObject ingredients;
    [SerializeField] private bool hasIngredient = false;

    void Start()
    {

    }

    void Update()
    {

    }

    private void ingredient()
    {
        if (hasIngredient)
        {
            Instantiate(ingredients, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasIngredient) {
            if (other.gameObject.CompareTag("Player"))
            {
                hasIngredient = true;
                ingredient();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && other.gameObject.CompareTag("Player") && hasIngredient == true)
        {
            hasIngredient = false;
        }
    }

}