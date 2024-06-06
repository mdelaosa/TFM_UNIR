using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class PickupDropObject : MonoBehaviour
{
    [Header("Pick up/Drop object")]
    [SerializeField] Player player;
    [SerializeField] private GameObject handPoint;
    [SerializeField] private GameObject pickedObject = null;
    [SerializeField] private bool hasObject = false;
    [SerializeField] private bool canDrop = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Drop();
    }

    /// <summary>
    /// Drop object
    /// </summary>
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

    /// <summary>
    /// Pick up object
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Wait one frame to prevent the same key from being used to pick up and drop in the same frame
    /// </summary>
    /// <returns></returns>
    IEnumerator DropObjectRoutine()
    {
        yield return null;
        
        canDrop = true;
    }
}




