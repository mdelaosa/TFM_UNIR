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
        
    }

    #region Getters and Setters
    public void AddPoints(int addedPoints)
    {
        points = points + addedPoints;
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

    #endregion
}
