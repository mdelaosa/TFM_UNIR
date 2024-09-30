using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public Order GenerateOrder(ClientController client)
    {
        RecipeName recipeName = GetRandomRecipeName();
        GameObject recipeObject = new GameObject("Order_" + recipeName);
        Order recipe = Order.CreateOrder(recipeObject, recipeName, client);

        return recipe;
    }

    private RecipeName GetRandomRecipeName()
    {
        Array values = Enum.GetValues(typeof(RecipeName));
        System.Random random = new System.Random();
        RecipeName randomRecipeName = (RecipeName)values.GetValue(random.Next(values.Length));
        return randomRecipeName;
    }
}
