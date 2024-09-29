using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private ChairID chairID;
    [SerializeField] private Table table; // Asigna la mesa correspondiente en el Inspector
    private ClientController currentClient;

    #region Getters y Setters

    public ChairID GetChairID()
    {
        return chairID;
    }

    public Table GetTable()
    {
        return table;
    }

    public void SetClient(ClientController client)
    {
        currentClient = client;
    }

    public void ClearClient()
    {
        currentClient = null;
    }

    public bool IsAvailable()
    {
        return currentClient == null;
    }

    #endregion
}
