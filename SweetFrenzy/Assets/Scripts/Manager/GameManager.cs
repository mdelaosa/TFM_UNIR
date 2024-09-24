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

    [Header("Player")]
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    private Vector3 player1SpawnPoint;
    private Vector3 player2SpawnPoint;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        isGameOver = false;
        UpdatePointsUI();

        player1SpawnPoint = new Vector3(0.310000002f, -1.65999997f, -6.28999996f);
        player2SpawnPoint = new Vector3(5.51000023f, -1.65999997f, -6.67999983f);
        SpawnPlayers();
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

    #region Spawn Players Function
    private void SpawnPlayers()
    {
        int playerCount = PlayerPrefs.GetInt("playersCount", 1);  // Obtener el número de jugadores (por defecto, 1)
        Debug.Log(playerCount);
        Instantiate(player1Prefab, player1SpawnPoint, Quaternion.identity);

        if (playerCount == 2)
        {
            Instantiate(player2Prefab, player2SpawnPoint, Quaternion.identity);
        }
    }
    #endregion
}
