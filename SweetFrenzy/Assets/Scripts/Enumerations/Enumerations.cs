using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodStatus
{
    raw,            
    cut,            
    mixed,          
    kneaded,        
    baked,          
    ready,          
    bowled,
    served
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
    mixAppleBanana,
    mixAppleStrawberry,
    mixStrawberryBanana,
    egg,
    milk,
    flour,
    dough,
    rawApplePie,
    applePie,
    fruitBowl,
    fruitSmoothie,
}

public enum RecipeName
{
    applePie,
    fruitBowl,
    fruitSmoothie
}

public enum UtensilName
{
    table,
    kneader,
    kneaderEgg,
    kneaderMilk,
    kneaderFlour,
    kneaderMixEggMilk,
    kneaderMixEggFlour,
    kneaderMixMilkFlour,
    kneaderNotMixDough,
    kneaderDough,
    bowl,
    knife
}

public enum UtensilStatus
{
    empty,
    full,
    preparedToWork,
    kneading,
    finished,
    none
}
