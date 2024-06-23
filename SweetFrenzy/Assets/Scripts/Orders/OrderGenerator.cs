using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public void GenerateOrder()
    {
        RecipeName recipeName = GetRandomRecipeName();
        GameObject recipeObject = new GameObject("Order_" + recipeName);
        Order recipe = Order.CreateOrder(recipeObject, recipeName);

        Debug.Log("Ingredientes para la receta de " + recipeName + ": " + string.Join(", ", recipe.GetIngredients()) + "...........Tiempo de entrega para la receta: " + recipe.GetDeliveryTime() + " segundos");
    }

    private RecipeName GetRandomRecipeName()
    {
        Array values = Enum.GetValues(typeof(RecipeName));
        System.Random random = new System.Random();
        RecipeName randomRecipeName = (RecipeName)values.GetValue(random.Next(values.Length));
        return randomRecipeName;
    }
}
