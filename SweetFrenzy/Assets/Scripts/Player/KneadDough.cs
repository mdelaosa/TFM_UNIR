using UnityEngine;

public class KneadDough : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Player player;
    private Kneader kneader;

    [Header("Booleans")]
    [SerializeField] private bool isTouchingKneader = false;
    [SerializeField] private bool isKneading = false;

    void Update()
    {
        CheckKneading();
    }

    private void CheckKneading()
    {
        if (!player.IsMoving() && isTouchingKneader && kneader != null && kneader.GetUtensilName() == UtensilName.kneaderNotMixDough)
        {
            if ((player.GetPlayerID() == PlayerID.player1 && Input.GetKey(KeyCode.E)) || (player.GetPlayerID() == PlayerID.player2 && Input.GetKey(KeyCode.Return)))
            {
                StartKneading();
            }
            else
            {
                StopKneading();
            }
        }
        else
        {
            StopKneading();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kneader"))
        {
            isTouchingKneader = true;
            kneader = other.GetComponent<Kneader>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Kneader"))
        {
            isTouchingKneader = false;
            kneader = null;
        }

        StopKneading();
    }

    private void StartKneading()
    {
        if (!isKneading && kneader != null)
        {
            isKneading = true;
            kneader.StartKneading();
        }
    }

    private void StopKneading()
    {
        if (isKneading && kneader != null)
        {
            isKneading = false;
            kneader.StopKneading();
        }
    }
}
