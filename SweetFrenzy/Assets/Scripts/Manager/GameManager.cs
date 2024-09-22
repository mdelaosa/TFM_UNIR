using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numOrders = 0; // Número de órdenes completadas
    private int points = 0; // Puntos acumulados
    private bool isGameOver; // Estado del juego

    private const int winningScore = 50; // Puntuación mínima para ganar

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen; // Canvas de fin de juego
    [SerializeField] private GameObject winScreen; // Pantalla de victoria
    [SerializeField] private GameObject loseScreen; // Pantalla de derrota
    [SerializeField] private TextMeshProUGUI pointsTextUI; // Texto de puntos en la UI durante la partida
    [SerializeField] private TextMeshProUGUI pointsTextGameOver; // Texto de puntos en la pantalla de fin de juego

    void Start()
    {
        isGameOver = false;

        // Asegurarse de que las pantallas de fin de juego estén desactivadas al inicio
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        // Inicializar el texto de puntos en la UI
        UpdatePointsUI();
    }

    void Update()
    {
        // Verificar si el juego ha terminado y determinar el resultado
        if (isGameOver)
        {
            CheckGameResult();
        }
    }

    #region Game Over Functions
    private void CheckGameResult()
    {
        // Mostrar la pantalla de fin de juego y la correspondiente pantalla de victoria o derrota
        if (points >= winningScore)
        {
            PlayerWon();
        }
        else
        {
            PlayerLost();
        }

        // Actualizar el texto de puntos en la pantalla de fin de juego
        UpdatePointsGameOver();
    }

    private void PlayerWon()
    {
        Debug.Log("¡Felicidades! Has ganado la partida con " + points + " puntos.");
        ShowEndGameScreen(true); // true indica que el jugador ganó
    }

    private void PlayerLost()
    {
        Debug.Log("Lo siento, no has alcanzado la puntuación mínima. Has perdido la partida.");
        ShowEndGameScreen(false); // false indica que el jugador perdió
    }

    private void ShowEndGameScreen(bool hasWon)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Activar el canvas de fin de juego
        }

        if (hasWon)
        {
            if (winScreen != null)
            {
                winScreen.SetActive(true); // Mostrar la pantalla de victoria
            }
        }
        else
        {
            if (loseScreen != null)
            {
                loseScreen.SetActive(true); // Mostrar la pantalla de derrota
            }
        }
    }
    #endregion

    #region Getters and Setters
    public void AddPoints(int addedPoints)
    {
        points += addedPoints;
        Debug.Log("Total points: " + points);
        UpdatePointsUI(); // Actualizar el texto de puntos en la UI
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
        Debug.Log("Estado del juego: " + (isGameOver ? "Terminado" : "En progreso"));

        if (isGameOver)
        {
            // Llamar a CheckGameResult() para evaluar el resultado del juego
            CheckGameResult();
        }
    }
    #endregion

    #region UI Update Functions
    // Actualiza el texto de puntos en la UI principal durante la partida
    private void UpdatePointsUI()
    {
        if (pointsTextUI != null)
        {
            pointsTextUI.text = "Puntos: " + points;
        }
    }

    // Actualiza el texto de puntos en la pantalla de fin de juego
    private void UpdatePointsGameOver()
    {
        if (pointsTextGameOver != null)
        {
            pointsTextGameOver.text = "Puntos Finales: " + points;
        }
    }
    #endregion
}
