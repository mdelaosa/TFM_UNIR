using System.Collections;
using UnityEngine;

public class Mixer : Utensil
{
    [Header("Mixing")]
    [SerializeField] private GameObject notMixedSmoothie;
    [SerializeField] private GameObject smoothieMixing;
    [SerializeField] private GameObject mixerSmoothiePrefab;
    private GameObject mixedContents;

    [Header("Progress Bar")]
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarVariable;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float progress = 0f;

    [Header("Timer")]
    private float timer = 0f;
    private float mixingDelay;
    private Coroutine mixingRoutine;

    private void Start()
    {
        utensilName = UtensilName.mixerNotMixSmoothie;
        utensilStatus = UtensilStatus.preparedToWork;
        notMixedSmoothie.SetActive(true);
        smoothieMixing.SetActive(false);
        progressBar.SetActive(false);
        mixingDelay = 3f; 

        initialScale = progressBarVariable.transform.localScale;
        initialPosition = progressBarVariable.transform.localPosition;
    }

    public void StartMixing()
    {
        if (utensilStatus != UtensilStatus.finished)
        {
            utensilStatus = UtensilStatus.mixing;
            mixingRoutine = StartCoroutine(MixingRoutine());
            notMixedSmoothie.SetActive(false);
            smoothieMixing.SetActive(true);
            progressBar.SetActive(true);
        }
    }

    public void StopMixing()
    {
        if (utensilStatus == UtensilStatus.mixing && mixingRoutine != null)
        {
            StopCoroutine(mixingRoutine);
            mixingRoutine = null;
        }
        if (utensilStatus == UtensilStatus.finished)
        {
            smoothieMixing.SetActive(false);
            progressBar.SetActive(false);

            mixedContents = Instantiate(mixerSmoothiePrefab);
            mixedContents.transform.position = transform.position;

            MixIngredients.Instance.HandleUtensilDestruction(gameObject);
        }
    }

    private IEnumerator MixingRoutine()
    {
        while (timer < mixingDelay)
        {
            progress = timer / mixingDelay;

            progressBarVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);
            progressBarVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);

            yield return null;
            timer += Time.deltaTime;
        }

        progressBar.SetActive(false);
        progressBarVariable.transform.localScale = initialScale;
        progressBarVariable.transform.localPosition = initialPosition;
        utensilStatus = UtensilStatus.finished;
    }
}
