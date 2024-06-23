using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixIngredients : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Player player;
    [SerializeField] private List<Food> ingredients = new List<Food>();
    [SerializeField] private List<Utensil> utensils = new List<Utensil>();
    private GameObject ingredientsMix = null;

    [Header("Player Handpoint")]
    [SerializeField] private Transform handPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject bowlApplePrefab;
    [SerializeField] private GameObject bowlStrawberryPrefab;
    [SerializeField] private GameObject bowlBananaPrefab;
    [SerializeField] private GameObject bowlMixAppleBananaPrefab;
    [SerializeField] private GameObject bowlMixAppleStrawberryPrefab;
    [SerializeField] private GameObject bowlMixStrawberryBananaPrefab;
    [SerializeField] private GameObject fruitBowlPrefab;
    [SerializeField] private GameObject bowlDoughPrefab;
    [SerializeField] private GameObject rawApplePiePrefab;

    [SerializeField] private GameObject emptyKneaderPrefab;
    [SerializeField] private GameObject kneaderEggPrefab;
    [SerializeField] private GameObject kneaderMilkPrefab;
    [SerializeField] private GameObject kneaderFlourPrefab;
    [SerializeField] private GameObject kneaderMixEggMilkPrefab;
    [SerializeField] private GameObject kneaderMixEggFlourPrefab;
    [SerializeField] private GameObject kneaderMixMilkFlourPrefab;
    [SerializeField] private GameObject kneaderNotMixDoughPrefab;

    [Header("Booleans")]
    [SerializeField] private bool isTouchingBowl = false;
    [SerializeField] private bool isMixing = false;

    private void Update()
    {
        Mix();
    }

    private void Mix()
    {
        if (!player.IsMoving() && isTouchingBowl)
        {
            if (player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartMixing();
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.RightControl))
            {
                StartMixing();
            }
            else
            {
                StopMixing();
            }
        }
        else
        {
            StopMixing();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bowl") || other.CompareTag("Kneader"))
        {
            isTouchingBowl = true;
            Utensil utensil = other.GetComponent<Utensil>();
            if (utensil != null && !utensils.Contains(utensil))
            {
                utensils.Add(utensil);
            }
        }

        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null && !ingredients.Contains(food))
            {
                ingredients.Add(food);
            }
        }

        if (other.CompareTag("BowlFruit"))
        {
            isTouchingBowl = true;

            Food food = other.GetComponent<Food>();
            if (food != null && !ingredients.Contains(food))
            {
                ingredients.Add(food);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bowl") || other.CompareTag("BowlFruit"))
        {
            isTouchingBowl = false;
        }

        Food food = other.GetComponent<Food>();
        if (food != null && ingredients.Contains(food))
        {
            ingredients.Remove(food);
        }

        Utensil utensil = other.GetComponent<Utensil>();
        if (utensil != null && utensils.Contains(utensil))
        {
            utensils.Remove(utensil);
        }

        if (!isTouchingBowl)
        {
            StopMixing();
        }
    }

    private void StartMixing()
    {
        if (!isMixing)
        {
            isMixing = true;
            CheckAndMixIngredients();
        }
    }

    private void StopMixing()
    {
        if (isMixing)
        {
            isMixing = false;
        }
    }

    private void CheckAndMixIngredients()
    {
        bool hasEgg = false, hasMilk = false, hasFlour = false;
        bool hasApple = false, hasStrawberry = false, hasBanana = false;
        bool hasBowlApple = false, hasBowlStrawberry = false, hasBowlBanana = false;
        bool hasBowlMixAppleBanana = false, hasBowlMixAppleStrawberry = false, hasBowlMixStrawberryBanana = false;
        bool hasKneaderEgg = false, hasKneaderMilk = false, hasKneaderFlour = false;
        bool hasKneaderMixEggMilk = false, hasKneaderMixEggFlour = false, hasKneaderMixMilkFlour = false;
        bool hasKneaderNotMixDough = false, hasKneaderDough = false, hasBowlDough = false;
        bool hasEmptyBowl = false, hasEmptyKneader = false;

        foreach (var ingredient in ingredients)
        {
            if (ingredient == null) continue;

            if (ingredient.GetFoodName() == FoodName.egg) hasEgg = true;
            else if (ingredient.GetFoodName() == FoodName.milk) hasMilk = true;
            else if (ingredient.GetFoodName() == FoodName.flour) hasFlour = true;

            else if (ingredient.GetFoodName() == FoodName.apple && ingredient.GetFoodStatus() == FoodStatus.cut) hasApple = true;
            else if (ingredient.GetFoodName() == FoodName.strawberry && ingredient.GetFoodStatus() == FoodStatus.cut) hasStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.banana && ingredient.GetFoodStatus() == FoodStatus.cut) hasBanana = true;

            else if (ingredient.GetFoodName() == FoodName.apple && ingredient.GetFoodStatus() == FoodStatus.ready) hasBowlApple = true;
            else if (ingredient.GetFoodName() == FoodName.strawberry && ingredient.GetFoodStatus() == FoodStatus.ready) hasBowlStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.banana && ingredient.GetFoodStatus() == FoodStatus.ready) hasBowlBanana = true;

            else if (ingredient.GetFoodName() == FoodName.mixedAppleBanana) hasBowlMixAppleBanana = true;
            else if (ingredient.GetFoodName() == FoodName.mixedAppleStrawberry) hasBowlMixAppleStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.mixedStrawberryBanana) hasBowlMixStrawberryBanana = true;
        }

        foreach (var utensil in utensils)
        {
            if (utensil == null) continue;

            if (utensil.GetUtensilName() == UtensilName.bowl && utensil.GetUtensilStatus() == UtensilStatus.empty) hasEmptyBowl = true;
            else if (utensil.GetUtensilName() == UtensilName.kneader && utensil.GetUtensilStatus() == UtensilStatus.empty) hasEmptyKneader = true;

            else if (utensil.GetUtensilName() == UtensilName.kneaderEgg) hasKneaderEgg = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMilk) hasKneaderMilk = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderFlour) hasKneaderFlour = true;

            else if (utensil.GetUtensilName() == UtensilName.kneaderMixEggMilk) hasKneaderMixEggMilk = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMixEggFlour) hasKneaderMixEggFlour = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMixMilkFlour) hasKneaderMixMilkFlour = true;

            else if (utensil.GetUtensilName() == UtensilName.kneaderNotMixDough) hasKneaderNotMixDough = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderDough) hasKneaderDough = true;
        }

        if (hasEmptyBowl && hasApple)
        {
            CreateIngredientsMix(bowlApplePrefab);
        }
        else if (hasEmptyBowl && hasStrawberry)
        {
            CreateIngredientsMix(bowlStrawberryPrefab);
        }
        else if (hasEmptyBowl && hasBanana)
        {
            CreateIngredientsMix(bowlBananaPrefab);
        }
        else if ((hasBowlApple && hasBanana) || (hasBowlBanana && hasApple))
        {
            CreateIngredientsMix(bowlMixAppleBananaPrefab);
        }
        else if ((hasBowlApple && hasStrawberry) || (hasBowlStrawberry && hasApple))
        {
            CreateIngredientsMix(bowlMixAppleStrawberryPrefab);
        }
        else if ((hasBowlStrawberry && hasBanana) || (hasBowlBanana && hasStrawberry))
        {
            CreateIngredientsMix(bowlMixStrawberryBananaPrefab);
        }
        else if ((hasBowlMixAppleBanana && hasStrawberry) || (hasBowlMixAppleStrawberry && hasBanana) || (hasBowlMixStrawberryBanana && hasApple))
        {
            CreateIngredientsMix(fruitBowlPrefab);
        }

        else if (hasEmptyKneader && hasEgg)
        {
            CreateIngredientsMix(kneaderEggPrefab);
        }
        else if (hasEmptyKneader && hasMilk)
        {
            CreateIngredientsMix(kneaderMilkPrefab);
        }
        else if (hasEmptyKneader && hasFlour)
        {
            CreateIngredientsMix(kneaderFlourPrefab);
        }
        else if ((hasKneaderEgg && hasMilk) || (hasKneaderMilk && hasEgg))
        {
            CreateIngredientsMix(kneaderMixEggMilkPrefab);
        }
        else if ((hasKneaderEgg && hasFlour) || (hasKneaderFlour && hasEgg))
        {
            CreateIngredientsMix(kneaderMixEggFlourPrefab);
        }
        else if ((hasKneaderMilk && hasFlour) || (hasKneaderFlour && hasMilk))
        {
            CreateIngredientsMix(kneaderMixMilkFlourPrefab);
        }
        else if ((hasKneaderMixEggMilk && hasFlour) || (hasKneaderMixEggFlour && hasMilk) || (hasKneaderMixMilkFlour && hasEgg))
        {
            CreateIngredientsMix(kneaderNotMixDoughPrefab);
        }
        else if (hasEmptyBowl && hasKneaderDough)
        {
            CreateIngredientsMixInHand(bowlDoughPrefab);
        }
        else if ((hasBowlDough && hasApple) || (hasBowlApple && hasKneaderDough))
        {
            CreateIngredientsMixInHand(rawApplePiePrefab);
        }
    }

    #region Create ingredients
    private void CreateIngredientsMix(GameObject mixPrefab)
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient != null)
            {
                Destroy(ingredient.gameObject);
            }
        }
        ingredients.Clear();

        foreach (var utensil in utensils)
        {
            if (utensil != null)
            {
                Destroy(utensil.gameObject);
            }
        }
        utensils.Clear();

        if (mixPrefab != null)
        {
            ingredientsMix = Instantiate(mixPrefab);
            ingredientsMix.transform.position = transform.position;
        }

        isMixing = false;
    }

    private void CreateIngredientsMixInHand(GameObject mixPrefab)
    {
        foreach (var ingredient in ingredients)
        {
            if (ingredient != null)
            {
                Destroy(ingredient.gameObject);
            }
        }
        ingredients.Clear();

        foreach (var utensil in utensils)
        {
            if (utensil != null)
            {
                if (utensil.CompareTag("Kneader"))
                {
                    GameObject emptyKneader = Instantiate(emptyKneaderPrefab, utensil.transform.position, utensil.transform.rotation);
                    Destroy(utensil.gameObject);
                }
                else
                {
                    Destroy(utensil.gameObject);
                }
            }
        }
        utensils.Clear();

        if (mixPrefab != null)
        {
            ingredientsMix = Instantiate(mixPrefab);
            ingredientsMix.transform.position = handPoint.position;
            ingredientsMix.transform.parent = handPoint;
        }

        isMixing = false;
    }
    #endregion
}
