using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] public GameObject bowls;

    public void GetBowl()
    {
        Instantiate(bowls, transform.position, Quaternion.identity);
    }
}
