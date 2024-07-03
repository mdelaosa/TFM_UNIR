using UnityEngine;

public class Kneader : BaseUtensil
{
    [Header("Knead")]
    [SerializeField] private GameObject notKneadedDough;
    [SerializeField] private GameObject kneaderWorking;
    [SerializeField] private GameObject kneaderDoughPrefab;

    protected override void Start()
    {
        notUtensilWorking = notKneadedDough;
        utensilWorking = kneaderWorking;
        utensilFinishedPrefab = kneaderDoughPrefab;
        base.Start();
        utensilName = UtensilName.kneaderNotMixDough;
        utensilStatus = UtensilStatus.preparedToWork;
    }
}
