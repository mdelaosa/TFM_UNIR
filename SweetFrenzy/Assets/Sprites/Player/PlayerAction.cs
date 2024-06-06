using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private bool isTouchingUtensil = false;
    private bool isTouchingFruit = false;
    private Fruit fruitScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Utensil"))
        {
            isTouchingUtensil = true;
        }

        if (other.CompareTag("Food"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.GetFoodStatus() == FoodStatus.raw)
            {
                isTouchingFruit = true;
                fruitScript = fruit;
            }
        }

        if (isTouchingUtensil && isTouchingFruit)
        {
            fruitScript.StartCutting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Utensil"))
        {
            isTouchingUtensil = false;
        }

        if (other.CompareTag("Food"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.GetFoodStatus() == FoodStatus.raw)
            {
                isTouchingFruit = false;
                fruitScript = null;
            }
        }

        if (!isTouchingUtensil || !isTouchingFruit)
        {
            if (fruitScript != null)
            {
                fruitScript.StopCutting();
            }
        }
    }
}
