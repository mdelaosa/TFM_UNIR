using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCliente : MonoBehaviour
{
    private RecipeGenerator recipeGenerator;
    [SerializeField] private int tiempo = 2;

    // Start is called before the first frame update
    void Start()
    {
        recipeGenerator = GetComponent<RecipeGenerator>();

        if (recipeGenerator == null)
        {
            Debug.LogError("RecipeGenerator no encontrado.");
            return;
        }

        StartCoroutine(CallGenerateRecipeEveryFiveSeconds());
    }

    void Update()
    {

    }

    private IEnumerator CallGenerateRecipeEveryFiveSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempo);
            recipeGenerator.GenerateRecipe();
        }
    }
}
