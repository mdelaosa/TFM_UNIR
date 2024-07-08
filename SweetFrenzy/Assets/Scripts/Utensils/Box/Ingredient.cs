using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Food
{
    [SerializeField] public GameObject ingredients;
    [SerializeField] private GameObject posIngredient;

    public void GetIngredient()
    {
        Instantiate(ingredients, posIngredient.transform.position, Quaternion.identity);
    }
}
