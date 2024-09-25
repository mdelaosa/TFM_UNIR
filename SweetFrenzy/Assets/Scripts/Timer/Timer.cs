using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer configuration")]
    [SerializeField] private float initialTime = 120f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timerImage;
    [SerializeField] private Color defaultColor = new Color32(165, 125, 159, 255);   // A57D9F
    [SerializeField] private Color warningColor = new Color32(244, 185, 96, 255);    // F4B960
    [SerializeField] private Color dangerColor = new Color32(233, 72, 74, 255);      // E9484A

    private float timeRemaining;
    private GameManager gameManager;
    private int lastDisplayedSeconds;

    void Start()
    {
        timeRemaining = initialTime;
        timerText.color = defaultColor;

        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        Vector3 originalPosition = timerText.transform.localPosition;

        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            if (seconds != lastDisplayedSeconds)
            {
                timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                lastDisplayedSeconds = seconds;
            }

            if (timeRemaining < 11)
            {
                timerText.color = dangerColor;

                float scaleModifier = 1 + Mathf.Sin((10 - timeRemaining) * Mathf.PI * 2) * 0.1f;
                timerText.transform.localScale = Vector3.one * scaleModifier;
            }
            else if (timeRemaining < 31)
            {
                timerText.color = warningColor;
            }
            else
            {
                timerText.color = defaultColor;
                timerText.transform.localScale = Vector3.one;
            }

            timeRemaining -= Time.deltaTime;

            yield return null;
        }

        timerText.transform.localPosition = originalPosition;
        EndGame();
    }

    void EndGame()
    {
        if (gameManager != null)
        {
            timerText.gameObject.SetActive(false);
            timerImage.SetActive(false);
            gameManager.SetGameOver(true);
        }
    }
}
