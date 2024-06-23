using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCliente : MonoBehaviour
{
    private OrderGenerator orderGenerator;
    [SerializeField] private int tiempo = 5;

    // Start is called before the first frame update
    void Start()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();

        //StartCoroutine(CallGenerateRecipeEveryFiveSeconds());
    }

    void Update()
    {

    }

    private IEnumerator CallGenerateRecipeEveryFiveSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempo);
            orderGenerator.GenerateOrder();
        }
    }
}
