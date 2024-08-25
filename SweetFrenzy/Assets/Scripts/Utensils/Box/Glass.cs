using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] public GameObject smoothieglass;
    [SerializeField] private GameObject posGlass;

    public void GetGlass()
    {
        Instantiate(smoothieglass, posGlass.transform.position, Quaternion.identity);
    }
}