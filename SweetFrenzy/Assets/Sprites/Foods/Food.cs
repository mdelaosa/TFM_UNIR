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
    raw,            // materia prima
    processed,      // procesado
    cooked          // postre
}

public enum FoodName
{
    apple,
    strawberry,
    banana,
    //nut,
    //cheese, 
    egg, 
    milk, 
    flour, 
    //coffee, z
    //chocolateBar,
    dough   //masa
}


public class Food : MonoBehaviour
{
    [SerializeField] protected FoodName foodName;
    [SerializeField] protected FoodStatus foodStatus;
    [SerializeField] protected FoodType foodType;
    [SerializeField] protected List<FoodStatus> possibleStatus = new List<FoodStatus>();
    //[SerializeField] private List<RecipeName> possibleRecipes;
    [SerializeField] private string foodTag = "Food";

    // Start is called before the first frame update
    void Start()
    {
        //PossibleStatus();
        gameObject.tag = foodTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //private void PossibleStatus()
    //{
    //    if ((foodName == FoodName.apple) || (foodName == FoodName.strawberry) || (foodName == FoodName.banana) || (foodName == FoodName.nut) || (foodName == FoodName.cheese) || (foodName == FoodName.egg) || (foodName == FoodName.milk) || (foodName == FoodName.flour) || (foodName == FoodName.coffee) || (foodName == FoodName.chocolateBar) || (foodName == FoodName.dough))
    //    {
    //        possibleStatus.Add(FoodStatus.raw);
    //        type = FoodType.raw;
    //    }

    //    if ((foodName == FoodName.apple) || (foodName == FoodName.strawberry) || (foodName == FoodName.banana) || (foodName == FoodName.nut) || (foodName == FoodName.cheese))
    //    {
    //        possibleStatus.Add(FoodStatus.cut);
    //    }

    //    if (foodName == FoodName.chocolateBar)
    //    {
    //        possibleStatus.Add(FoodStatus.melted);
    //    }
    //}

    //public void SetStatus(FoodStatus newStatus)
    //{
    //    if (possibleStatus.Find(newStatus))
    //    {
    //        status = newStatus;
    //    }
    //}

    #region Getters and setters

    public FoodStatus GetFoodStatus()
    {
        return foodStatus;
    }
    #endregion
}
