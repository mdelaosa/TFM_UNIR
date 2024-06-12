using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private Player player;
    private Fruit fruit;

    [Header("Booleans")]
    [SerializeField] private bool isTouchingUtensil = false;
    [SerializeField] private bool isTouchingFruit = false;
    [SerializeField] private bool isCutting = false;

    private void Update()
    {
        Cut();
    }

    private void Cut()
    {
        if (isTouchingUtensil && isTouchingFruit && fruit != null)
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
            Fruit fruitCollider = other.GetComponent<Fruit>();
            if (fruitCollider != null && fruitCollider.GetFoodStatus() == FoodStatus.raw)
            {
                isTouchingFruit = true;
                fruit = fruitCollider;
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
            Fruit fruitCollider = other.GetComponent<Fruit>();
            if (fruitCollider != null && fruitCollider.GetFoodStatus() == FoodStatus.raw)
            {
                isTouchingFruit = false;
                fruit = null;
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
            fruit.StartCutting();
        }
    }

    private void StopCutting()
    {
        if (isCutting && fruit != null)
        {
            isCutting = false;
            fruit.StopCutting();
        }
    }
}
