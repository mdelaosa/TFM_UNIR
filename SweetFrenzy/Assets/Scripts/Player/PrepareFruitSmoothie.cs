using UnityEngine;

public class PrepareFruitSmoothie : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Player player;
    private Mixer mixer;

    [Header("Booleans")]
    [SerializeField] private bool isTouchingMixer = false;
    [SerializeField] private bool isMixing = false;

    void Update()
    {
        CheckMixing();
    }

    private void CheckMixing()
    {
        if (!player.IsMoving() && isTouchingMixer && mixer != null && mixer.GetUtensilName() == UtensilName.mixerNotMixSmoothie)
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
        if (other.CompareTag("Mixer"))
        {
            isTouchingMixer = true;
            mixer = other.GetComponent<Mixer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mixer"))
        {
            isTouchingMixer = false;
            mixer = null;
        }

        StopMixing();
    }

    private void StartMixing()
    {
        if (!isMixing && mixer != null)
        {
            isMixing = true;
            mixer.StartMixing();
        }
    }

    private void StopMixing()
    {
        if (isMixing && mixer != null)
        {
            isMixing = false;
            mixer.StopMixing();
        }
    }
}
