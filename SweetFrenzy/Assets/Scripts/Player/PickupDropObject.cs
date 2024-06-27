using System.Collections;
using UnityEngine;

public class PickupDropObject : MonoBehaviour
{
    [Header("Pick up/Drop object")]
    [SerializeField] Player player;
    [SerializeField] private GameObject handPoint;
    private GameObject pickedObject = null;
    private bool hasObject = false;
    private bool canDrop = false;

    void Update()
    {
        Drop();
    }

    private void Drop()
    {
        bool dropInput;

        if (player.GetPlayerID() == PlayerID.player1)
        {
            dropInput = Input.GetKeyDown(KeyCode.LeftControl);
        }
        else
        {
            dropInput = Input.GetKeyDown(KeyCode.RightControl);
        }

        if (dropInput && hasObject && canDrop)
        {
            if (pickedObject != null)
            {
                Rigidbody pickedObjectRb = pickedObject.GetComponent<Rigidbody>();
                if (pickedObjectRb != null)
                {
                    pickedObjectRb.useGravity = true;
                    pickedObjectRb.isKinematic = false;
                    pickedObject.transform.SetParent(null);
                }
                pickedObject = null;
                hasObject = false;
                canDrop = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool pickupInput;

        if (player.GetPlayerID() == PlayerID.player1)
        {
            pickupInput = Input.GetKeyDown(KeyCode.LeftControl);
        }
        else
        {
            pickupInput = Input.GetKeyDown(KeyCode.RightControl);
        }

        if ((other.gameObject.CompareTag("Food") || other.gameObject.CompareTag("BowlFruit") || other.gameObject.CompareTag("Bowl") || other.gameObject.CompareTag("Glass")) && pickupInput && !hasObject)
        {
            StartCoroutine(PickupDropRoutine(other));
        }
    }

    IEnumerator PickupDropRoutine(Collider other)
    {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if (otherRb != null)
        {
            otherRb.useGravity = false;
            otherRb.isKinematic = true;
        }
        other.transform.position = handPoint.transform.position;
        other.transform.SetParent(handPoint.transform);

        pickedObject = other.gameObject;
        hasObject = true;

        yield return StartCoroutine(DropObjectRoutine());
    }

    IEnumerator DropObjectRoutine()
    {
        yield return null;

        canDrop = true;
    }
}
