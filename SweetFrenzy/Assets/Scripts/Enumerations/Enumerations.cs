using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodStatus
{
    raw,            // crudo
    cut,            // cortado
    mixed,          // mezclado
    kneaded,        // amasado
    baked,          // horneado
    ready,          // listo
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
    mixedAppleBanana,
    mixedAppleStrawberry,
    mixedStrawberryBanana,
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
    kneading,
    none
}
