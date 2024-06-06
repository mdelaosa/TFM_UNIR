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
        if (isTouchingUtensil && isTouchingFruit && fruitScript != null)
        {
            if (player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E))
            {
                StartCutting();
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return))
            {
                StartCutting();
            }
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
            StopCutting();
        }

        if (other.CompareTag("Food"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null && fruit.GetFoodStatus() == FoodStatus.raw)
            {
                isTouchingFruit = false;
                StopCutting();
                fruitScript = null;
            }
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
