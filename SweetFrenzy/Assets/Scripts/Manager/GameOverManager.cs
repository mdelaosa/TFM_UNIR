using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI pointsTextGameOver;

    void Start()
    {
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
    }

    public void PlayerWon(int points)
    {
        Debug.Log("¡Felicidades! Has ganado la partida con " + points + " puntos.");
        ShowEndGameScreen(true, points);
    }

    public void PlayerLost(int points)
    {
        Debug.Log("Lo siento, no has alcanzado la puntuación mínima. Has perdido la partida.");
        ShowEndGameScreen(false, points);
    }

    private void ShowEndGameScreen(bool hasWon, int points)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        if (pointsTextGameOver != null)
        {
            pointsTextGameOver.text = "Puntos Finales: " + points;
        }

        if (hasWon)
        {
            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
        else
        {
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }
        }
    }
}
