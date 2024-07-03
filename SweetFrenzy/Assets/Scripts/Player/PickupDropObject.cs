using System.Collections;
using UnityEngine;

public class PickupDropObject : MonoBehaviour
{
    [Header("Pick up/Drop object")]
    [SerializeField] Player player;
    [SerializeField] private GameObject handPoint;
    public GameObject pickedObject = null; // Cambiar a public para acceso desde BakeApplePie
    [SerializeField] public bool hasObject = false;
    [SerializeField] private bool canDrop = false;

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
            pickedObject.GetComponent<Rigidbody>().useGravity = true;
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.transform.SetParent(null);
            pickedObject = null;
            hasObject = false;
            canDrop = false;
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

        if ((other.gameObject.CompareTag("Food")) || (other.gameObject.CompareTag("Bowl")) || (other.gameObject.CompareTag("Glass")))
        {
            if (pickupInput && !hasObject)
            {
                StartCoroutine(PickupDropRoutine(other));
            }
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

    public void PickupObject(GameObject obj)
    {
        pickedObject = obj;
        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);
        hasObject = true;
        canDrop = false;
    }

    #region Getters and Setters
    public GameObject GetPickedObject()
    {
        return pickedObject;
    }
    
    public void SetHasObjectStatus(bool newHasObjectSatus)
    {
        hasObject = newHasObjectSatus;
    }

    #endregion
}
