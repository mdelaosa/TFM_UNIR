using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenuSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Función para un jugador
    public void StartOnePlayerGame()
    {
        Debug.Log("1 PLAYER");
        PlayerPrefs.SetInt("playersCount", 1);
        SceneManager.LoadScene("Level1");
    }

    // Función para dos jugadores
    public void StartTwoPlayerGame()
    {
        Debug.Log("2 PLAYERS");
        PlayerPrefs.SetInt("playersCount", 2);
        SceneManager.LoadScene("Level1");
    }
}
