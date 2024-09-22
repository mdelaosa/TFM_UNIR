using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int numOrders = 0; // N�mero de �rdenes completadas
    private int points = 0; // Puntos acumulados
    private bool isGameOver; // Estado del juego

    // Puntuaci�n m�nima para ganar
    private const int winningScore = 50;

    void Start()
    {
        isGameOver = false;
    }

    void Update()
    {

    }

    #region Game Over Functions
    private void CheckGameResult()
    {
        if (points >= winningScore)
        {
            PlayerWon();
        }
        else
        {
            PlayerLost();
        }
    }

    private void PlayerWon()
    {
        Debug.Log("�Felicidades! Has ganado la partida con " + points + " puntos.");
        ShowEndGameScreen(true); 
    }

    private void PlayerLost()
    {
        Debug.Log("Lo siento, no has alcanzado la puntuaci�n m�nima. Has perdido la partida.");
        ShowEndGameScreen(false);
    }

    private void ShowEndGameScreen(bool hasWon)
    {
        if (hasWon)
        {
            Debug.Log("Mostrando pantalla de victoria.");
        }
        else
        {
            Debug.Log("Mostrando pantalla de derrota.");
        }
    }
    #endregion

    #region Getters and Setters
    public void AddPoints(int addedPoints)
    {
        points += addedPoints;
        Debug.Log("Total points: " + points);
    }

    public void AddOrder()
    {
        numOrders++;
        Debug.Log("Total orders: " + numOrders);
    }

    public int GetNumOrders()
    {
        return numOrders;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;

        if (isGameOver)
        {
            CheckGameResult();
        }
    }
    #endregion
}
