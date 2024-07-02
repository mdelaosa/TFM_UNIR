using System.Collections;
using UnityEngine;

public class Kneader : Utensil
{
    [Header("Knead")]
    [SerializeField] private GameObject notKneadedDough;
    [SerializeField] private GameObject doughKneading;
    [SerializeField] private GameObject kneaderDoughPrefab;
    private GameObject doughFinished;

    [Header("Progress Bar")]
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarVariable;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float progress = 0f;

    [Header("Timer")]
    private float timer = 0f;
    private float kneadingDelay;
    private Coroutine kneadingRoutine;

    private void Start()
    {
        utensilName = UtensilName.kneaderNotMixDough;
        utensilStatus = UtensilStatus.preparedToWork;

        if (notKneadedDough != null) notKneadedDough.SetActive(true);
        if (doughKneading != null) doughKneading.SetActive(false);
        if (progressBar != null) progressBar.SetActive(false);

        kneadingDelay = 2f;

        if (progressBarVariable != null)
        {
            initialScale = progressBarVariable.transform.localScale;
            initialPosition = progressBarVariable.transform.localPosition;
        }
    }

    public void StartKneading()
    {
        if (utensilStatus != UtensilStatus.finished)
        {
            utensilStatus = UtensilStatus.kneading;
            if (kneadingRoutine == null)
            {
                kneadingRoutine = StartCoroutine(KneadingRoutine());
            }
            if (notKneadedDough != null) notKneadedDough.SetActive(false);
            if (doughKneading != null) doughKneading.SetActive(true);
            if (progressBar != null) progressBar.SetActive(true);
        }
    }

    public void StopKneading()
    {
        if (utensilStatus == UtensilStatus.kneading && kneadingRoutine != null)
        {
            StopCoroutine(kneadingRoutine);
            kneadingRoutine = null;
            utensilStatus = UtensilStatus.preparedToWork;
        }
    }

    private IEnumerator KneadingRoutine()
    {
        while (timer < kneadingDelay)
        {
            progress = timer / kneadingDelay;

            if (progressBarVariable != null)
            {
                progressBarVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);
                progressBarVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);
            }

            yield return null;
            timer += Time.deltaTime;
        }

        progressBar.SetActive(false);
        progressBarVariable.transform.localScale = initialScale;
        progressBarVariable.transform.localPosition = initialPosition;
        utensilStatus = UtensilStatus.finished;
        kneadingRoutine = null;

        if (doughKneading != null) doughKneading.SetActive(false);
        if (progressBar != null) progressBar.SetActive(false);

        if (kneaderDoughPrefab != null)
        {
            doughFinished = Instantiate(kneaderDoughPrefab);
            doughFinished.transform.position = transform.position;
        }

        if (MixIngredients.Instance != null)
        {
            MixIngredients.Instance.HandleUtensilDestruction(gameObject);
        }

        Destroy(gameObject);
    }
}
