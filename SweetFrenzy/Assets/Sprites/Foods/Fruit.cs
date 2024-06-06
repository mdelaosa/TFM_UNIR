using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Food
{
    [SerializeField] protected bool isRaw = true;
    [SerializeField] protected bool isCut = false;
    [SerializeField] protected GameObject fruitRaw;
    [SerializeField] protected GameObject fruitCut;
    [SerializeField] protected float cutDelay;
    protected Coroutine cutRoutine;

    // Start is called before the first frame update
    void Start()
    {
        foodType = FoodType.raw;
        foodStatus = FoodStatus.raw;
        fruitRaw.SetActive(true);
        fruitCut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCutting()
    {
        if (cutRoutine == null)
        {
            cutRoutine = StartCoroutine(CutFruitRoutine());
        }
    }

    public void StopCutting()
    {
        if (cutRoutine != null)
        {
            StopCoroutine(cutRoutine);
            cutRoutine = null;
        }
    }

    private IEnumerator CutFruitRoutine()
    {
        yield return new WaitForSeconds(cutDelay);
        foodStatus = FoodStatus.cut;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (foodStatus == FoodStatus.cut)
        {
            fruitRaw.SetActive(false);
            fruitCut.SetActive(true);
        }
    }
}
