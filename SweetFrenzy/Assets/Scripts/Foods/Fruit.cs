using System.Collections;
using UnityEngine;

public class Fruit : Food
{
    [Header("Cut")]
    [SerializeField] private GameObject fruitRaw;
    [SerializeField] private GameObject fruitIsBeingCut;
    [SerializeField] private GameObject fruitCut;

    [Header("Progress Bar")]
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarVariable;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private float progress = 0f;

    [Header("Timer")]
    private float timer = 0f;
    private float cutDelay;
    private Coroutine cutRoutine;
     

    private void Start()
    {
        foodType = FoodType.raw;
        //foodStatus = FoodStatus.raw;
        fruitRaw.SetActive(true);
        fruitCut.SetActive(false);
        progressBar.SetActive(false);
        cutDelay = 2;

        initialScale = progressBarVariable.transform.localScale;
        initialPosition = progressBarVariable.transform.localPosition;
    }

    public void StartCutting()
    {
        if ((cutRoutine == null) && (foodType != FoodType.processed))
        {
            cutRoutine = StartCoroutine(CutFruitRoutine());
            progressBar.SetActive(true);
        }
    }


    public void StopCutting()
    {
        if (cutRoutine != null)
        {
            StopCoroutine(cutRoutine);
            cutRoutine = null;
            if (foodStatus == FoodStatus.cut)
            {
                progressBar.SetActive(false);
            }
        }
    }

    private IEnumerator CutFruitRoutine()
    {
        UpdateSprite();

        while (timer < cutDelay)
        {
            progress = timer / cutDelay;

            progressBarVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);

            progressBarVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);

            yield return null;
            timer += Time.deltaTime;
        }

        foodStatus = FoodStatus.cut;
        foodType = FoodType.processed;
        UpdateSprite();
        progressBar.SetActive(false);
        progressBarVariable.transform.localScale = initialScale;
        progressBarVariable.transform.localPosition = initialPosition;
    }

    private void UpdateSprite()
    {
        if (foodStatus == FoodStatus.raw)
        {
            fruitRaw.SetActive(false);
            fruitIsBeingCut.SetActive(true);
            fruitCut.SetActive(false);
        }
        else if (foodStatus == FoodStatus.cut)
        {
            fruitRaw.SetActive(false);
            fruitIsBeingCut.SetActive(false);
            fruitCut.SetActive(true);
        }
    }
}
