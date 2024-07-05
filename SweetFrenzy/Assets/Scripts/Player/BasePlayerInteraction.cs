using UnityEngine;

public abstract class BasePlayerInteraction : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] protected Player player;
    protected BaseUtensil utensil;

    [Header("Booleans")]
    [SerializeField] protected bool isTouchingUtensil = false;
    [SerializeField] protected bool isProcessing = false;

    void Update()
    {
        CheckInteraction();
    }

    protected abstract void CheckInteraction();

    protected virtual void OnTriggerEnter(Collider other)
    {
        utensil = other.GetComponentInParent<BaseUtensil>();
        if (utensil != null)
        {
            isTouchingUtensil = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<BaseUtensil>() == utensil)
        {
            isTouchingUtensil = false;
            utensil = null;
        }

        StopInteraction();
    }

    protected void StartInteraction()
    {
        if (!isProcessing && utensil != null)
        {
            isProcessing = true;
            utensil.StartProcess();
        }
    }

    protected void StopInteraction()
    {
        if (isProcessing && utensil != null)
        {
            isProcessing = false;
            utensil.StopProcess();
        }
    }
}
