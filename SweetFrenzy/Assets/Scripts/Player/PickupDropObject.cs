using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class PickupDropObject : MonoBehaviour
{
    [Header("Pick up/Drop object")]
    [SerializeField] Player player;
    [SerializeField] private GameObject handPoint;
    private GameObject pickedObject = null;
    private bool hasObject = false;
    private bool canDrop = false;

    void Start()
    {

    }

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
            pickedObject.gameObject.transform.SetParent(null);
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

        if (other.gameObject.CompareTag("Food"))
        {
            if (pickupInput && !hasObject)
            {
                StartCoroutine(PickupDropRoutine(other));
            }
        }
    }

    IEnumerator PickupDropRoutine(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = false;
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.transform.position = handPoint.transform.position;
        other.gameObject.transform.SetParent(handPoint.gameObject.transform);

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