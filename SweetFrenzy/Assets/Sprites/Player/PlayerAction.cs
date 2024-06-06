using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private bool isTouchingUtensil = false;
    [SerializeField] private bool isTouchingFruit = false;
    [SerializeField] private bool isCutting = false;
    [SerializeField] private Fruit fruitScript;

    private void Update()
    {
        Cut();
    }

    private void Cut()
    {
        if (isTouchingUtensil && isTouchingFruit && fruitScript != null)
        {
            if (player.GetPlayerID() == PlayerID.player1 && Input.GetKey(KeyCode.E))
            {
                StartCutting();
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKey(KeyCode.Return))
            {
                StartCutting();
            }
            else
            {
                StopCutting();
            }
        }
        else
        {
            StopCutting();
        }
    }

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
            StopCutting();
        }
    }

    private void StartCutting()
    {
        if (!isCutting)
        {
            isCutting = true;
            fruitScript.StartCutting();
        }
    }

    private void StopCutting()
    {
        if (isCutting && fruitScript != null)
        {
            isCutting = false;
            fruitScript.StopCutting();
        }
    }
}
