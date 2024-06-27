using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRecipe : MonoBehaviour
{
    private OrderGenerator orderGenerator;

    private Order recipeOrdered;

    void Start()
    {

    }

    public Order Order()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();

        if (orderGenerator == null)
        {
            Debug.LogError("RecipeGenerator no encontrado.");
            return null;
        }

        recipeOrdered = orderGenerator.GenerateOrder();
        return recipeOrdered;
    }
}
