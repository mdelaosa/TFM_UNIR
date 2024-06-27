using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Food
{
    [SerializeField] public GameObject ingredients;

    public void GetIngredient()
    {
        Instantiate(ingredients, transform.position, Quaternion.identity);
    }
}
