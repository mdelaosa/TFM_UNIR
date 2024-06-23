using System.Collections;
using UnityEngine;


public class Kneader : Utensil
{
    //[Header("Prefabs")]
    //[SerializeField] private GameObject doughRaw;
    //[SerializeField] private GameObject doughKneading;
    //[SerializeField] private GameObject doughFinished;

    //[Header("Progress Bar")]
    //[SerializeField] private GameObject progressBar;
    //[SerializeField] private GameObject progressBarVariable;
    //private Vector3 initialScale;
    //private Vector3 initialPosition;
    //private float timer = 0f;
    //private float progress = 0f;

    //private float kneadingDelay = 5f;
    //private float kneadingTimer = 0f;
    //private Coroutine kneadingRoutine;

    //private void Start()
    //{
    //    doughRaw.SetActive(true);
    //    doughKneading.SetActive(false);
    //    doughFinished.SetActive(false);
    //    progressBar.SetActive(false);

    //    initialScale = progressBarVariable.transform.localScale;
    //    initialPosition = progressBarVariable.transform.localPosition;
    //}

    //public void StartKneading()
    //{
    //    if (utensilStatus == UtensilStatus.empty)
    //    {
    //        utensilStatus = UtensilStatus.kneading;
    //        kneadingRoutine = StartCoroutine(KneadingRoutine());
    //        doughRaw.SetActive(false);
    //        doughKneading.SetActive(true);
    //        progressBar.SetActive(true);
    //    }
    //}

    //public void StopKneading()
    //{
    //    if (utensilStatus == UtensilStatus.kneading && kneadingRoutine != null)
    //    {
    //        StopCoroutine(kneadingRoutine);
    //        utensilStatus = UtensilStatus.full;
    //        kneadingTimer = 0f;
    //        doughKneading.SetActive(false);
    //        doughRaw.SetActive(true);
    //        progressBar.SetActive(false);
    //    }
    //}

    //private IEnumerator KneadingRoutine()
    //{
    //    while (kneadingTimer < kneadingDelay)
    //    {
    //        progress = kneadingTimer / kneadingDelay;

    //        progressBarVariable.transform.localScale = new Vector3(initialScale.x * progress, initialScale.y, initialScale.z);
    //        progressBarVariable.transform.localPosition = new Vector3(initialPosition.x - initialScale.x * 0.5f * (1 - progress), initialPosition.y, initialPosition.z);

    //        yield return null;
    //        kneadingTimer += Time.deltaTime;
    //    }

    //    doughKneading.SetActive(false);
    //    doughFinished.SetActive(true);
    //    utensilStatus = UtensilStatus.full;
    //    progressBar.SetActive(false);
    //}
}
