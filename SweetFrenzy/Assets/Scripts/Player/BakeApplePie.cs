using UnityEngine;

public class BakeApplePie : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] protected Player player;
    [SerializeField] private Oven utensil;

    [Header("Booleans")]
    [SerializeField] protected bool isTouchingUtensil = false;
    [SerializeField] protected bool isProcessing = false;

    [Header("PickupDropObject")]
    [SerializeField] private PickupDropObject pickupDropObject;

    private void Start()
    {
        if (pickupDropObject == null)
        {
            Debug.LogError("PickupDropObject component is missing from the player.");
        }
    }

    private void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (!player.IsMoving() && isTouchingUtensil && utensil != null && utensil.GetUtensilName() == UtensilName.oven)
        {
            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.LeftControl)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.RightControl)))
            {
                if (utensil.GetUtensilStatus() == UtensilStatus.empty)
                {
                    PutRawApplePieInOven();
                }
                else if (utensil.GetUtensilStatus() == UtensilStatus.finished)
                {
                    TakeApplePieOfOven();
                }
            }

            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return)))
            {
                if (utensil.GetUtensilStatus() == UtensilStatus.preparedToWork)
                {
                    TurnOnOven();
                }
                else if (utensil.GetUtensilStatus() == UtensilStatus.burning)
                {
                    TurnOffOven();
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        utensil = other.GetComponent<Oven>();
        if (utensil != null)
        {
            isTouchingUtensil = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Oven>() == utensil)
        {
            isTouchingUtensil = false;
            utensil = null;
        }
    }

    private void PutRawApplePieInOven()
    {
        if (pickupDropObject == null)
        {
            Debug.LogError("pickupDropObject is not initialized.");
            return;
        }

        if (utensil != null && utensil.GetUtensilStatus() == UtensilStatus.empty && pickupDropObject.GetHasObjectStatus())
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
                utensil.InsertFood(); 
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
        utensil.TurnOnOven();
    }

    private void TurnOffOven()
    {
        utensil.TurnOffOven();
    }

    private void TakeApplePieOfOven()
    {
        if (utensil.GetUtensilStatus() == UtensilStatus.finished)
        {
            GameObject applePie = utensil.TakeOutFood();
            pickupDropObject.PickupObject(applePie);
        }
    }
}
