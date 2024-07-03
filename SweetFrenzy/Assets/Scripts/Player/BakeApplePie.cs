using UnityEngine;

public class BakeApplePie : BasePlayerInteraction
{
    private PickupDropObject pickupDropObject;

    private void Start()
    {
        pickupDropObject = player.GetComponent<PickupDropObject>();
    }

    protected override void CheckInteraction()
    {
        if (!player.IsMoving() && isTouchingUtensil && utensil != null && utensil.GetUtensilName() == UtensilName.oven)
        {
            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.E)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.Return)))
            {
                AttemptToBake();
            }

            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKeyDown(KeyCode.LeftControl)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKeyDown(KeyCode.RightControl)))
            {
                AttemptToTakeOut();
            }
        }
    }

    private void AttemptToBake()
    {
        if (utensil is Oven oven && oven.GetUtensilStatus() == UtensilStatus.empty && pickupDropObject.hasObject)
        {
            GameObject applePie = pickupDropObject.GetPickedObject();
            Food applePieFood = applePie.GetComponent<Food>();
            if (applePieFood != null && applePieFood.GetFoodName() == FoodName.rawApplePie)
            {
                oven.InsertFood(applePie); // Insertar el pastel de manzana en el horno
                pickupDropObject.SetHasObjectStatus(false); // Actualizar el estado del jugador
                applePie.transform.SetParent(null); // Desvincular el objeto de la mano del jugador
            }
        }
    }

    private void AttemptToTakeOut()
    {
        if (utensil is Oven oven && oven.GetUtensilStatus() == UtensilStatus.finished)
        {
            // Sacar el pastel de manzana del horno
            GameObject applePie = oven.TakeOutFood();
            if (applePie != null)
            {
                // Realizar acciones con el pastel de manzana obtenido
                Food applePieFood = applePie.GetComponent<Food>();
                if (applePieFood != null)
                {
                    if (applePieFood.GetFoodStatus() == FoodStatus.ready)
                    {
                        Debug.Log("¡Pastel de manzana listo para servir!");
                    }
                    else if (applePieFood.GetFoodStatus() == FoodStatus.burnt)
                    {
                        Debug.Log("¡El pastel de manzana se ha quemado!");
                    }
                }
                // Aquí puedes decidir si el jugador recoge automáticamente el pastel o simplemente lo deja en el horno
                // pickupDropObject.PickupObject(applePie); // Ejemplo de recolección automática (necesitarías implementar este método)
            }
        }
    }
}
