using UnityEngine;

public class BakeApplePie : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] protected Player player;

    [Header("Booleans")]
    [SerializeField] protected bool isTouchingUtensil = false;
    [SerializeField] protected bool isProcessing = false;

    [Header("PickupDropObject")]
    [SerializeField] private PickupDropObject pickupDropObject;

    private Oven oven;

    private void Start()
    {

    }

    private void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (!player.IsMoving() && isTouchingUtensil && oven != null && oven.GetUtensilName() == UtensilName.oven)
        {
            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.LeftControl)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.RightControl)))
            {
                if (oven.GetUtensilStatus() == UtensilStatus.empty)
                {
                    PutRawApplePieInOven();
                }
                else if (oven.GetUtensilStatus() == UtensilStatus.finished)
                {
                    TakeApplePieOfOven();
                }
            }

            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return)))
            {
                if (oven.GetUtensilStatus() == UtensilStatus.preparedToWork)
                {
                    TurnOnOven();
                }
                else if (oven.GetUtensilStatus() == UtensilStatus.burning)
                {
                    TurnOffOven();
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        oven = other.GetComponent<Oven>();
        if (oven != null)
        {
            isTouchingUtensil = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Oven>() == oven)
        {
            isTouchingUtensil = false;
            oven = null;
        }
    }

    private void PutRawApplePieInOven()
    {
        if (pickupDropObject == null)
        {
            Debug.LogError("pickupDropObject is not initialized.");
            return;
        }

        if (oven != null && oven.GetUtensilStatus() == UtensilStatus.empty && pickupDropObject.GetHasObjectStatus())
        {
            GameObject rawApplePieObject = pickupDropObject.GetPickedObject();

            if (rawApplePieObject == null)
            {
                Debug.LogError("No object picked up by the player.");
                return;
            }

            Food rawApplePieFood = rawApplePieObject.GetComponent<Food>();
            if (rawApplePieFood.GetFoodName() == FoodName.rawApplePie)
            {
                oven.InsertFood(); 
                pickupDropObject.SetHasObjectStatus(false);
                Destroy(rawApplePieObject);
            }
            else
            {
                Debug.LogError("Picked object is not a raw apple pie.");
            }
        }
    }

    private void TurnOnOven()
    {
        oven.TurnOnOven();
    }

    private void TurnOffOven()
    {
        oven.TurnOffOven();
    }

    private void TakeApplePieOfOven()
    {
        if (oven.GetUtensilStatus() == UtensilStatus.finished)
        {
            GameObject applePie = oven.TakeOutFood();
            pickupDropObject.PickupObject(applePie);
        }
    }
}
