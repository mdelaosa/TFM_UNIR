using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recicle : Food
{
    [SerializeField] public GameObject ingredients;

    public void RecicleFood()
    {
        Destroy(gameObject);
    }
}
