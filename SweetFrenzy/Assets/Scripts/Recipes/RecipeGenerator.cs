using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRecipe()
    {
        RecipeName recipeName = GetRandomRecipeName();
        GameObject recipeObject = new GameObject("Recipe_" + recipeName);
        Recipe recipe = Recipe.CreateRecipe(recipeObject, recipeName);

        Debug.Log("Ingredientes para la receta de apple pie: " + string.Join(", ", recipe.GetIngredients()));
        Debug.Log("Tiempo de entrega para la receta: " + recipe.GetDeliveryTime() + " segundos");
    }

    private RecipeName GetRandomRecipeName()
    {
        Array values = Enum.GetValues(typeof(RecipeName));
        System.Random random = new System.Random();
        RecipeName randomRecipeName = (RecipeName)values.GetValue(random.Next(values.Length));
        return randomRecipeName;
    }
}
