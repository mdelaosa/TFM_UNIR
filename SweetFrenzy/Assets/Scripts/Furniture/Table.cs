using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private TableID tableID;
    [SerializeField] private List<Order> activeOrders = new List<Order>();

    [Header("Audio")]
    [SerializeField] private AudioClip correctSound;   
    [SerializeField] private AudioClip incorrectSound; 
    private AudioSource audioSource;                   

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Food food = other.gameObject.GetComponent<Food>();
            if (food != null && food.GetFoodStatus() == FoodStatus.ready && food.GetFoodType() == FoodType.cooked)
            {
                food.SetFoodStatus(FoodStatus.served);
                Debug.Log($"Food {food.GetFoodName()} is now served.");

                FulfillOrder(other.gameObject);
            }
            else
            {
                if (gameManager != null)
                {
                    gameManager.AddPoints(-10);
                }
                PlaySound(incorrectSound);
                Destroy(other.gameObject);
            }
        }
    }

    public void AddOrder(Order order)
    {
        activeOrders.Add(order);
    }

    public void RemoveOrder(Order order)
    {
        activeOrders.Remove(order);
    }

    public void FulfillOrder(GameObject other)
    {
        Food servedFood = other.GetComponent<Food>();

        if (servedFood == null)
        {
            Debug.LogWarning("No se ha encontrado un componente Food en el objeto proporcionado.");
            return;
        }

        bool isCorrect = false;

        foreach (var order in activeOrders)
        {
            if (order.GetRecipeName().ToString() == servedFood.GetFoodName().ToString())
            {
                isCorrect = true;
                order.GetClient().OnOrderReceived();
                order.SetIsReady(true);
                activeOrders.Remove(order);                
                PlaySound(correctSound);
                Debug.Log($"{correctSound.name}. Pedido cumplido y eliminado de la lista.");
                break;
            }
        }

        if (!isCorrect)
        {
            if (gameManager != null)
            {
                gameManager.AddPoints(-10);
            }
            Debug.Log("Pedido incorrecto");
            PlaySound(incorrectSound); 
            Destroy(other.gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); 
        }
    }
}
