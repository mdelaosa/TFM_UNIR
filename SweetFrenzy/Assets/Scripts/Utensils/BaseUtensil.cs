using System.Collections;
using UnityEngine;

public abstract class BaseUtensil : Utensil
{
    [Header("Utensil Working States")]
    protected GameObject notUtensilWorking;
    protected GameObject utensilWorking;
    protected GameObject utensilFinishedPrefab;
    private GameObject utensilFinished;

    [Header("Progress Bar")]
    [SerializeField] protected GameObject progressBar;
    [SerializeField] protected GameObject progressBarVariable;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float progress = 0f;
    private Coroutine processRoutine;

    [Header("Timer")]
    private float timer = 0f;
    private float processDelay = 2f;

    protected virtual void Start()
    {
        if (progressBarVariable != null)
        {
            initialScale = progressBarVariable.transform.localScale;
            initialPosition = progressBarVariable.transform.localPosition;
        }
        if (progressBar != null) progressBar.SetActive(false);

        if (notUtensilWorking != null) notUtensilWorking.SetActive(true);
        if (utensilWorking != null) utensilWorking.SetActive(false);
    }

    public void StartProcess()
    {
        if (utensilStatus != UtensilStatus.finished)
        {
            utensilStatus = UtensilStatus.working;
            if (processRoutine == null)
            {
                processRoutine = StartCoroutine(ProcessRoutine());
            }
            if (progressBar != null) progressBar.SetActive(true);
            if (notUtensilWorking != null) notUtensilWorking.SetActive(false);
            if (utensilWorking != null) utensilWorking.SetActive(true);
        }
    }

    public void StopProcess()
    {
        if (utensilStatus == UtensilStatus.working && processRoutine != null)
        {
            StopCoroutine(processRoutine);
            processRoutine = null;
            utensilStatus = UtensilStatus.preparedToWork;
        }
    }

    private IEnumerator ProcessRoutine()
    {
        while (timer < processDelay)
        {
            progress = timer / processDelay;
            if (progressBarVariable != null)
            {
                progressBarVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);
                progressBarVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);
            }
            yield return null;
            timer += Time.deltaTime;
        }

        utensilStatus = UtensilStatus.finished;
        processRoutine = null;
        if (progressBar != null) progressBar.SetActive(false);
        if (progressBarVariable != null)
        {
            progressBarVariable.transform.localScale = initialScale;
            progressBarVariable.transform.localPosition = initialPosition;
        }

        OnProcessFinished();
    }

    private void OnProcessFinished()
    {
        if (utensilWorking != null) utensilWorking.SetActive(false);

        if (utensilFinishedPrefab != null)
        {
            utensilFinished = Instantiate(utensilFinishedPrefab);
            utensilFinished.transform.position = transform.position;
            utensilFinished.transform.rotation = transform.rotation;
        }

        if (MixIngredients.Instance != null)
        {
            MixIngredients.Instance.HandleUtensilDestruction(gameObject);
        }

        Destroy(gameObject);
    }
}
