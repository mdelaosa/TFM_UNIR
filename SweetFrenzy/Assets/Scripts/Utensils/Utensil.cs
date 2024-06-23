using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utensil : MonoBehaviour
{
    [SerializeField] protected UtensilName utensilName;
    [SerializeField] protected UtensilStatus utensilStatus;

    #region Getters and setters

    public void SetUtensilStatus(UtensilStatus newUtensilSatus)
    {
        utensilStatus = newUtensilSatus;
    }

    public UtensilStatus GetUtensilStatus()
    {
        return utensilStatus;
    }

    public UtensilName GetUtensilName()
    {
        return utensilName;
    }
    #endregion
}
