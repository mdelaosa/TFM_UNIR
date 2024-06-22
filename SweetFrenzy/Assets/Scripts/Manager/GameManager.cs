using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int numOrders = 0;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public int GetNumOrders()
    {
        return numOrders;
    }

    #endregion
}
