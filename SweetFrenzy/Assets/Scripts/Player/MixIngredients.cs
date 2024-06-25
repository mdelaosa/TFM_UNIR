using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixIngredients : MonoBehaviour
{
    public static MixIngredients Instance { get; private set; }

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        bool hasEmptyBowl = false;
        bool hasBowlApple = false, hasBowlStrawberry = false, hasBowlBanana = false;
        bool hasBowlMixAppleBanana = false, hasBowlMixAppleStrawberry = false, hasBowlMixStrawberryBanana = false;

        bool hasEmptyKneader = false;
        bool hasKneaderEgg = false, hasKneaderMilk = false, hasKneaderFlour = false;
        bool hasKneaderMixEggMilk = false, hasKneaderMixEggFlour = false, hasKneaderMixMilkFlour = false;
        bool hasKneaderDough = false, hasBowlDough = false;

        foreach (var ingredient in ingredients)
        {
            if (ingredient == null) continue;

            if (ingredient.GetFoodName() == FoodName.egg && ingredient.GetFoodStatus() == FoodStatus.raw) hasEgg = true;
            else if (ingredient.GetFoodName() == FoodName.milk && ingredient.GetFoodStatus() == FoodStatus.raw) hasMilk = true;
            else if (ingredient.GetFoodName() == FoodName.flour && ingredient.GetFoodStatus() == FoodStatus.raw) hasFlour = true;
            else if (ingredient.GetFoodName() == FoodName.apple && ingredient.GetFoodStatus() == FoodStatus.raw) hasApple = true;
            else if (ingredient.GetFoodName() == FoodName.strawberry && ingredient.GetFoodStatus() == FoodStatus.raw) hasStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.banana && ingredient.GetFoodStatus() == FoodStatus.raw) hasBanana = true;

            else if (ingredient.GetFoodName() == FoodName.apple && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlApple = true;
            else if (ingredient.GetFoodName() == FoodName.strawberry && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.banana && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlBanana = true;
            else if (ingredient.GetFoodName() == FoodName.dough && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlDough = true;

            else if (ingredient.GetFoodName() == FoodName.mixAppleBanana && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlMixAppleBanana = true;
            else if (ingredient.GetFoodName() == FoodName.mixAppleStrawberry && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlMixAppleStrawberry = true;
            else if (ingredient.GetFoodName() == FoodName.mixStrawberryBanana && ingredient.GetFoodStatus() == FoodStatus.bowled) hasBowlMixStrawberryBanana = true;
        }

        foreach (var utensil in utensils)
        {
            if (utensil == null) continue;

            if (utensil.GetUtensilName() == UtensilName.bowl) hasEmptyBowl = true;
            else if (utensil.GetUtensilName() == UtensilName.kneader) hasEmptyKneader = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderEgg) hasKneaderEgg = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMilk) hasKneaderMilk = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderFlour) hasKneaderFlour = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMixEggMilk) hasKneaderMixEggMilk = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMixEggFlour) hasKneaderMixEggFlour = true;
            else if (utensil.GetUtensilName() == UtensilName.kneaderMixMilkFlour) hasKneaderMixMilkFlour = true;
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

        foreach (var utensil in utensils.ToList())
        {
            if (utensil != null)
            {
                Destroy(utensil.gameObject);
            }
            utensils.Remove(utensil);
        }

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

        foreach (var utensil in utensils.ToList())
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
            utensils.Remove(utensil);
        }

        if (mixPrefab != null)
        {
            ingredientsMix = Instantiate(mixPrefab);
            ingredientsMix.transform.position = handPoint.position;
            ingredientsMix.transform.parent = handPoint;
        }

        isMixing = false;
    }

    public void HandleUtensilDestruction(GameObject utensil)
    {
        if (utensils.Contains(utensil.GetComponent<Utensil>()))
        {
            utensils.Remove(utensil.GetComponent<Utensil>());
            Destroy(utensil);
        }
    }
}
