using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateClient());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InstantiateClient()
    {
        while (true)
        {
            Instantiate(clientPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }
}
