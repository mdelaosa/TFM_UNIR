using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRecipe : MonoBehaviour
{
    private OrderGenerator orderGenerator;

    void Start()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();

        if (orderGenerator == null)
        {
            Debug.LogError("RecipeGenerator no encontrado.");
            return;
        }

        orderGenerator.GenerateOrder();
    }
}
