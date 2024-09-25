using UnityEngine;

public class Mixer : BaseUtensil
{
    [Header("Mixing")]
    [SerializeField] private GameObject notMixedSmoothie;
    [SerializeField] private GameObject mixerWorking;
    [SerializeField] private GameObject mixerSmoothiePrefab;

    protected override void Start()
    {
        notUtensilWorking = notMixedSmoothie;
        utensilWorking = mixerWorking;
        utensilFinishedPrefab = mixerSmoothiePrefab;
        base.Start();
        utensilName = UtensilName.mixer;
        utensilStatus = UtensilStatus.empty;
    }
}
