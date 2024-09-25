using System.Collections;
using System.Data;
using UnityEngine;

public class Oven : Utensil
{
    [Header("Progress Bar")]
    [SerializeField] protected GameObject progressBar;
    [SerializeField] protected GameObject progressBarVariable;

    [Header("Burn Bar")]
    [SerializeField] private GameObject burnBar;
    [SerializeField] private GameObject burnBarVariable;
    [SerializeField] Vector3 initialScale;
    [SerializeField] Vector3 initialPosition;

    [Header("Apple Pie Icons")]
    [SerializeField] private GameObject rawApplePieIcon;
    [SerializeField] private GameObject applePieIcon;
    [SerializeField] private GameObject burntApplePieIcon;

    [Header("Apple Pie Prefabs")]
    [SerializeField] private GameObject applePiePrefab;
    [SerializeField] private GameObject burntApplePiePrefab;

    [Header("Oven Settings")]
    [SerializeField] private bool isBaking = false;
    [SerializeField] private bool isBurning = false;
    [SerializeField] private bool isApplePieBurnt = false;

    [Header("Timer")]
    [SerializeField] private float bakingTimer = 0f;
    [SerializeField] private float burnTimer = 0f;
    [SerializeField] private float bakingProgress = 0f;
    [SerializeField] private float burnProgress = 0f;
    private float bakingProcessDelay = 5f;
    private float burnProcessDelay = 5f;

    private Coroutine processRoutine;

    private void Start()
    {
        utensilName = UtensilName.oven;
        utensilStatus = UtensilStatus.empty;

        initialScale = progressBarVariable.transform.localScale;
        initialPosition = progressBarVariable.transform.localPosition;

        UpdateUtensilState();
    }

    #region 1º step -> Put apple pie in the oven
    public void InsertFood()
    {
        if (utensilStatus == UtensilStatus.empty)
        {
            utensilStatus = UtensilStatus.preparedToWork;
            UpdateUtensilState();
        }
    }
    #endregion

    #region 2º step -> turn on the oven
    public void TurnOnOven()
    {
        if (!isBaking && !isBurning)
        {
            isBaking = true;
            bakingTimer = 0f;
            progressBar.SetActive(true);
            processRoutine = StartCoroutine(ProcessRoutine(progressBar, progressBarVariable, bakingTimer, bakingProcessDelay, bakingProgress));
            utensilStatus = UtensilStatus.working;
            UpdateUtensilState();
        }
    }

    private void FinishBaking()
    {
        StopBaking();
        StartBurn();
        utensilStatus = UtensilStatus.burning;
        UpdateUtensilState();
    }

    public void StopBaking()
    {
        if (isBaking)
        {
            isBaking = false;
            if (processRoutine != null)
            {
                StopCoroutine(processRoutine);
                processRoutine = null;
            }
            progressBar.SetActive(false);
        }
    }

    private void StartBurn()
    {
        if (!isBurning && !isBaking)
        {
            isBurning = true;
            burnTimer = 0f;
            burnBar.SetActive(true);
            utensilStatus = UtensilStatus.burning;
            processRoutine = StartCoroutine(ProcessRoutine(burnBar, burnBarVariable, burnTimer, burnProcessDelay, burnProgress));            
            UpdateUtensilState();
        }
    }
    #endregion

    #region 3º step -> turn off the oven
    public void TurnOffOven()
    {
        if (isBurning && (utensilStatus == UtensilStatus.burning))
        {
            isBurning = false;
            processRoutine = null;
            utensilStatus = UtensilStatus.finished;
            UpdateUtensilState();
            if (isApplePieBurnt)
            {
                burntApplePieIcon.SetActive(true);
            }
            else
            {
                applePieIcon.SetActive(true);
            }
        }
    }

    
    #endregion

    #region 4º step -> Take apple pie out of the oven
    public GameObject TakeOutFood()
    {
        if (utensilStatus == UtensilStatus.finished && applePiePrefab != null)
        {            
            utensilStatus = UtensilStatus.empty;
            UpdateUtensilState();
            ResetTimers();

            GameObject applePie;

            if (isApplePieBurnt)
            {
                applePie = Instantiate(burntApplePiePrefab);
                TransformFood(FoodStatus.burnt);
            }
            else
            {
                applePie = Instantiate(applePiePrefab);
                TransformFood(FoodStatus.ready);
            }
            // applePiePrefab.SetActive(true);

            return applePie;
        }
        return null;
    }

    private void TransformFood(FoodStatus newStatus)
    {
        if (applePiePrefab != null)
        {
            Food foodItem = applePiePrefab.GetComponent<Food>();
            if (foodItem != null)
            {
                foodItem.SetFoodStatus(newStatus);
            }
        }
    }

    private void ResetTimers()
    {
        bakingTimer = 0f;
        burnTimer = 0f;
        bakingProgress = 0f;
        burnProgress = 0f;
    }
    #endregion

    private void UpdateUtensilState()
    {
        if (utensilStatus == UtensilStatus.empty)
        {
            rawApplePieIcon.SetActive(false);
            progressBar.SetActive(false);
            burnBar.SetActive(false);
            applePieIcon.SetActive(false);
            burntApplePieIcon.SetActive(false);
        }
        else if (utensilStatus == UtensilStatus.preparedToWork)
        {
            rawApplePieIcon.SetActive(true);
            progressBar.SetActive(false);
            burnBar.SetActive(false);
            applePieIcon.SetActive(false);
            burntApplePieIcon.SetActive(false);
        }
        else if (utensilStatus == UtensilStatus.working)
        {
            rawApplePieIcon.SetActive(false);
            progressBar.SetActive(true);
            burnBar.SetActive(false);
            applePieIcon.SetActive(false);
            burntApplePieIcon.SetActive(false);
        }
        else if (utensilStatus == UtensilStatus.burning)
        {
            rawApplePieIcon.SetActive(false);
            progressBar.SetActive(false);
            burnBar.SetActive(true);
            applePieIcon.SetActive(false);
            burntApplePieIcon.SetActive(false);
        }
        else if (utensilStatus == UtensilStatus.finished)
        {
            rawApplePieIcon.SetActive(false);
            progressBar.SetActive(false);
            burnBar.SetActive(false);
        }
    }


    private IEnumerator ProcessRoutine(GameObject bar, GameObject barVariable, float timer, float processDelay, float progress)
    {
        while (timer < processDelay)
        {
            progress = timer / processDelay;
            if (barVariable != null)
            {
                
                barVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);
                barVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);
            }
            yield return null;
            timer += Time.deltaTime;
        }

        processRoutine = null;

        if (barVariable != null)
        {
            barVariable.transform.localScale = initialScale;
            barVariable.transform.localPosition = initialPosition;
        }

        if (bar.CompareTag("ProgressBar"))
        {
            bar.SetActive(false);
            FinishBaking();
        }
        else if (bar.CompareTag("BurnBar"))
        {
            isApplePieBurnt = true;
        }
    }
}
