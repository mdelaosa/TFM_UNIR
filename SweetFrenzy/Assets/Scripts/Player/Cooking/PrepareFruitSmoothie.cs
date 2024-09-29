using UnityEngine;

public class PrepareFruitSmoothie : BasePlayerInteraction
{
    protected override void CheckInteraction()
    {
        if (!player.IsMoving() && isTouchingUtensil && utensil != null && utensil.GetUtensilName() == UtensilName.mixerNotMixSmoothie)
        {
            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKey(KeyCode.E)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKey(KeyCode.Return)))
            {
                StartInteraction();
            }
            else
            {
                StopInteraction();
            }
        }
        else
        {
            StopInteraction();
        }
    }
}