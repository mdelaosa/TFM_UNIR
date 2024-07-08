using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    [SerializeField] public GameObject bowls;
    [SerializeField] private GameObject posBowl;

    public void GetBowl()
    {
        Instantiate(bowls, posBowl.transform.position, Quaternion.identity);
    }
}
