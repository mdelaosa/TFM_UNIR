using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixDoughAndApple : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Player player;
    private Food dough;
    private Food cutApple;

    [Header("Booleans")]
    [SerializeField] private bool isTouchingBowl = false;
    [SerializeField] private bool isMixing = false;

    private void Update()
    {
        Mix();
    }

    private void Mix()
    {
        if (!player.IsMoving() && isTouchingBowl && dough != null && cutApple != null)
        {
            if (player.GetPlayerID() == PlayerID.player1 && Input.GetKey(KeyCode.E))
            {
                StartMixing();
            }
            else if (player.GetPlayerID() == PlayerID.player2 && Input.GetKey(KeyCode.Return))
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
        }

        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null)
            {
                if (food.GetFoodName() == FoodName.dough && food.GetFoodStatus() == FoodStatus.mixed)
                {
                    dough = food;
                }
                else if (food.GetFoodName() == FoodName.apple && food.GetFoodStatus() == FoodStatus.cut)
                {
                    cutApple = food;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bowl"))
        {
            isTouchingBowl = false;
        }

        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null)
            {
                if (food.GetFoodName() == FoodName.dough)
                {
                    dough = null;
                }
                else if (food.GetFoodName() == FoodName.apple)
                {
                    cutApple = null;
                }
            }
        }

        if (!isTouchingBowl || dough == null || cutApple == null)
        {
            StopMixing();
        }
    }

    private void StartMixing()
    {
        if (!isMixing)
        {
            isMixing = true;
            CreateRawApplePie();
        }
    }

    private void StopMixing()
    {
        if (isMixing)
        {
            isMixing = false;
        }
    }

    private void CreateRawApplePie()
    {
        // Desactivar el dough y la manzana cortada
        dough.gameObject.SetActive(false);
        cutApple.gameObject.SetActive(false);

        // Crear el rawApplePie
        GameObject rawApplePie = Instantiate(Resources.Load("Prefabs/RawApplePie")) as GameObject;
        rawApplePie.transform.position = transform.position; // Posicionar el rawApplePie en el bowl

        isMixing = false;
    }
}
