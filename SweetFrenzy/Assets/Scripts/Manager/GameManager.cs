using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numOrders = 0;
    private int points = 0;
    private bool isGameOver;

    private const int winningScore = 50;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI pointsTextUI;
    private GameOverManager gameOverManager;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        isGameOver = false;
        UpdatePointsUI();
    }

    #region Game Over Functions
    public void CheckGameResult()
    {
        if (isGameOver)
        {
            if (points >= winningScore)
            {
                gameOverManager.PlayerWon(points);
            }
            else
            {
                gameOverManager.PlayerLost(points);
            }
        }
    }
    #endregion

    #region Getters and Setters
    public void AddPoints(int addedPoints)
    {
        points += addedPoints;
        Debug.Log("Total points: " + points);
        UpdatePointsUI();
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

    #region UI Update Functions
    private void UpdatePointsUI()
    {
        if (pointsTextUI != null)
        {
            pointsTextUI.text = "Puntos: " + points;
        }
    }
    #endregion
}
