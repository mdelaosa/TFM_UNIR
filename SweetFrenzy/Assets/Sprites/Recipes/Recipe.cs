using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RecipeName
{
    applePie,       // tarta de manzana
    //pancakes,       // tortitas
    //brownie,        // brownie
    fruitBowl,      // bol de frutas
    fruitSmoothie,  // batido de frutas
    //hotChocolate,   // chocolate caliente
    //coffee,         // cafe
    //cheesecake      // tarta de queso
}


public class Recipe : MonoBehaviour
{
    [SerializeField] private RecipeName recipeName;
    [SerializeField] List<FoodName> ingredients = new List<FoodName>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
