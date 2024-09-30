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
    burnt,          
    bowled,
    ready,
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
    knife,
    oven,
    glass,
    mixer,
    mixerMilk,
    mixerStrawberry,
    mixerBanana,
    mixerMixMilkStrawberry,
    mixerMixMilkBanana,
    mixerMixStrawberryBanana,
    mixerNotMixSmoothie,
    mixerSmoothie
}

public enum UtensilStatus
{
    empty,
    full,
    preparedToWork,
    working,
    burning,
    finished,
    none
}

public enum TableID
{
    table1 = 1,
    table2 = 2
}

public enum ChairID
{
    chair1 = 1,
    chair2 = 2,
    chair3 = 3,
    chair4 = 4
}
