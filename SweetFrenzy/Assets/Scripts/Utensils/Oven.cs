using System.Collections;
using UnityEngine;

public class Oven : BaseUtensil
{
    [Header("Burn bar")]
    [SerializeField] private GameObject burnBar;
    [SerializeField] private GameObject burnBarVariable;

    [Header("Oven Settings")]
    [SerializeField] private FoodName allowedFoodType = FoodName.rawApplePie; // Tipo de comida permitido en el horno
    [SerializeField] private float bakingTime = 5f; // Tiempo de horneado en segundos
    [SerializeField] private float burnTime = 3f; // Tiempo de enfriamiento antes de quemarse en segundos
    

    private bool isBaking = false;
    private bool isBurning = false;
    private float bakingTimer = 0f;
    private float burnTimer = 0f;
    private Coroutine bakingCoroutine;

    private GameObject foodObject; // Objeto de comida que está en el horno

    protected override void Start()
    {
        base.Start();
        utensilName = UtensilName.oven;
        utensilStatus = UtensilStatus.empty;
        UpdateUtensilState();
    }

    void Update()
    {
        if (isBaking)
        {
            bakingTimer += Time.deltaTime;
            float progress = bakingTimer / bakingTime;
            UpdateProgressBar(progressBarVariable, progress);

            if (bakingTimer >= bakingTime)
            {
                FinishBaking();
            }
        }
        else if (isBurning)
        {
            burnTimer += Time.deltaTime;
            float progress = burnTimer / burnTime;
            UpdateProgressBar(burnBarVariable, progress);

            if (burnTimer >= burnTime)
            {
                BurnFood();
            }
        }
    }

    public void InsertFood(GameObject food)
    {
        if (utensilStatus == UtensilStatus.empty && food.GetComponent<Food>().GetFoodName() == allowedFoodType)
        {
            foodObject = food;
            utensilStatus = UtensilStatus.working;
            UpdateUtensilState();
        }
    }

    public GameObject TakeOutFood()
    {
        if (utensilStatus == UtensilStatus.finished && foodObject != null)
        {
            GameObject finishedFood = foodObject;
            foodObject = null;
            utensilStatus = UtensilStatus.empty;
            UpdateUtensilState();
            return finishedFood;
        }
        return null;
    }

    public void StartBaking()
    {
        if (!isBaking && !isBurning && foodObject != null)
        {
            isBaking = true;
            progressBar.SetActive(true);
            bakingCoroutine = StartCoroutine(BakingRoutine());
        }
    }

    public void StopBaking()
    {
        if (isBaking)
        {
            isBaking = false;
            if (bakingCoroutine != null)
            {
                StopCoroutine(bakingCoroutine);
                bakingCoroutine = null;
            }
            progressBar.SetActive(false);
        }
    }

    private void StartBurn()
    {
        if (!isBurning && isBaking)
        {
            isBurning = true;
            burnBar.SetActive(true);
        }
    }

    private void StopBurn()
    {
        if (isBurning)
        {
            isBurning = false;
            burnTimer = 0f;
            burnBar.SetActive(false);
            if (foodObject != null)
            {
                Food foodItem = foodObject.GetComponent<Food>();
                if (foodItem != null)
                {
                    foodItem.SetFoodStatus(FoodStatus.ready);
                }
            }
        }
    }

    private IEnumerator BakingRoutine()
    {
        while (bakingTimer < bakingTime)
        {
            yield return null;
        }

        FinishBaking();
    }

    private void FinishBaking()
    {
        StopBaking();
        StartBurn();
        utensilStatus = UtensilStatus.finished;
        UpdateUtensilState();
    }

    private void BurnFood()
    {
        StopBurn();
        if (foodObject != null)
        {
            Food foodItem = foodObject.GetComponent<Food>();
            if (foodItem != null)
            {
                foodItem.SetFoodStatus(FoodStatus.burnt);
            }
        }
        foodObject = null;
        utensilStatus = UtensilStatus.empty;
        UpdateUtensilState();
    }

    private void UpdateUtensilState()
    {
        switch (utensilStatus)
        {
            case UtensilStatus.empty:
                progressBar.SetActive(false);
                burnBar.SetActive(false);
                break;
            case UtensilStatus.working:
                progressBar.SetActive(true);
                burnBar.SetActive(false);
                break;
            case UtensilStatus.finished:
                progressBar.SetActive(false);
                burnBar.SetActive(true);
                break;
        }
    }

    private void UpdateProgressBar(GameObject progressBarObject, float progress)
    {
        if (progressBarObject != null)
        {
            Transform barTransform = progressBarObject.transform.Find("Bar");
            if (barTransform != null)
            {
                RectTransform rectTransform = barTransform.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.localScale = new Vector3(progress, 1f, 1f);
                }
            }
        }
    }

    #region Getters and Setters
    public UtensilStatus GetUtensilStatus()
    {
        return utensilStatus;
    }
    #endregion
}
