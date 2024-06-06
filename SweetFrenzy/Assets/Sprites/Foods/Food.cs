using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
