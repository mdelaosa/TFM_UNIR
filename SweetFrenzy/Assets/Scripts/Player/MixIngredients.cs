using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixIngredients : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Player player;
    [SerializeField] private List<Food> ingredients = new List<Food>();
    [SerializeField] private List<GameObject> utensils = new List<GameObject>();
    [SerializeField] private GameObject ingredientsMix = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject bowlApplePrefab;
    [SerializeField] private GameObject bowlStrawberryPrefab;
    [SerializeField] private GameObject bowlBananaPrefab;
    [SerializeField] private GameObject bowlMixAppleBananaPrefab;
    [SerializeField] private GameObject bowlMixAppleStrawberryPrefab;
    [SerializeField] private GameObject bowlMixStrawberryBananaPrefab;
    [SerializeField] private GameObject fruitBowlPrefab;

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
        if (other.CompareTag("Bowl"))
        {
            isTouchingBowl = true;
            if (!utensils.Contains(other.gameObject))
            {
                utensils.Add(other.gameObject);
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

        if (utensils.Contains(other.gameObject))
        {
            utensils.Remove(other.gameObject);
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
        bool hasMixedAppleBanana = false, hasMixedAppleStrawberry = false, hasMixedStrawberryBanana = false;
        bool hasEmptyBowl = false;

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

            else if (ingredient.GetFoodName() == FoodName.mixedAppleBanana) hasMixedAppleBanana = true;
            else if (ingredient.GetFoodName() == FoodName.mixedAppleStrawberry) hasMixedAppleStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.mixedStrawberryBanana) hasMixedStrawberryBanana = true;
        }

        foreach (var utensil in utensils)
        {
            if (utensil == null) continue;

            if (utensil.CompareTag("Bowl")) hasEmptyBowl = true;
        }

        // Esto iría en la amasadora
        // if (hasEgg && hasMilk && hasFlour)
        // {
        //     CreateIngredientsMix("Dough");
        // }
        // esto iría en la batidora
        // else if (hasMilk && hasStrawberry && hasBanana)
        // {
        //     CreateIngredientsMix("FruitSmoothie");
        // }

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
        else if ((hasMixedAppleBanana && hasStrawberry) || (hasMixedAppleStrawberry && hasBanana) || (hasMixedStrawberryBanana && hasApple))
        {
            CreateIngredientsMix(fruitBowlPrefab);
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
                Destroy(utensil);
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
    #endregion
}
