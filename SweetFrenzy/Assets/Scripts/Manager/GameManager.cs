using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int numOrders = 0;
    private int points = 0;
    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (isGameOver)
        {
            bool hasWon = points >= winningScore;
            gameOverManager.ShowFinishedMessageAndBackground(hasWon, points);
        }
=======
        
>>>>>>> parent of f78ca840 (Resolve merge conflict by incorporating both suggestions)
    }

    #region Getters and Setters
    public void AddPoints(int addedPoints)
    {
<<<<<<< HEAD
        points += addedPoints;
        UpdatePointsUI();
=======
        points = points + addedPoints;
        Debug.Log("Total points: " + points);
>>>>>>> parent of f78ca840 (Resolve merge conflict by incorporating both suggestions)
    }

    public void AddOrder()
    {
        numOrders++;
    }

    public int GetNumOrders()
    {
        return numOrders;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

<<<<<<< HEAD
    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;

        if (isGameOver)
        {
            pointsTextUI.gameObject.SetActive(false);
            CheckGameResult();
        }
    }
    #endregion

    #region UI Update Functions
    private void UpdatePointsUI()
    {
        if (pointsTextUI != null)
        {
            pointsTextUI.text = "Points: " + points;
        }
    }
=======
>>>>>>> parent of f78ca840 (Resolve merge conflict by incorporating both suggestions)
    #endregion
}
