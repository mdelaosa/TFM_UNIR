using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] GameObject[] clientsPrefab;
    [SerializeField]  private GameManager gameManager;

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
            int random = Random.Range(0, clientsPrefab.Length);

            Instantiate(clientsPrefab[random], transform.position, Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }
}
