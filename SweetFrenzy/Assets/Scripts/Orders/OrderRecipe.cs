using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRecipe : MonoBehaviour
{
    private OrderGenerator orderGenerator;

    private Order recipeOrdered;

    public Order Order(ClientController client)
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();

        if (orderGenerator == null)
        {
            Debug.LogError("RecipeGenerator no encontrado.");
            return null;
        }

        recipeOrdered = orderGenerator.GenerateOrder(client);
        return recipeOrdered;
    }
}
