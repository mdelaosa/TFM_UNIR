using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private GameObject[] clientsPrefab;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateClient());
        //gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator InstantiateClient()
    {
        while (true && !gameManager.IsGameOver())
        {
            // Comprobar si hay sillas libres
            if (AreChairsAvailable())
            {
                int random = Random.Range(0, clientsPrefab.Length);
                Instantiate(clientsPrefab[random], transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    private bool AreChairsAvailable()
    {
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Chair");
        foreach (GameObject chair in chairs)
        {
            Chair chairComponent = chair.GetComponent<Chair>();
            if (chairComponent != null && chairComponent.IsAvailable())
            {
                return true; // Hay al menos una silla libre
            }
        }
        return false; // No hay sillas libres
    }
}
