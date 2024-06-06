using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodStatus
{
    raw,            // crudo
    cut,            // cortado
    mixed,          // mezclado
    kneaded,        // amasado
    cooked,         // cocinado
    baked,          // horneado
    melted,         // fundido
    ready           // listo
}

public enum FoodType
{
    raw,            
    processed,      
    cooked          
}

public enum FoodName
{
    apple,
    strawberry,
    banana, 
    egg, 
    milk, 
    flour, 
    dough   
}


public class Food : MonoBehaviour
{
    [SerializeField] protected FoodName foodName;
    [SerializeField] protected FoodStatus foodStatus;
    [SerializeField] protected FoodType foodType;
    [SerializeField] private string foodTag = "Food";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = foodTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region Getters and setters

    public FoodStatus GetFoodStatus()
    {
        return foodStatus;
    }
    #endregion
}
