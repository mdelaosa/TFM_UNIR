using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer configuration")]
    [SerializeField] private float initialTime = 120f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;

    private float timeRemaining;
    private GameManager gameManager;

    void Start()
    {
        timeRemaining = initialTime;
        timerText.color = defaultColor;

        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            if (timeRemaining <= 10f)
            {
                timerText.color = dangerColor;
                timerText.transform.localScale = Vector3.one * (1 + Mathf.Sin(Time.time * 10) * 0.1f);
            }
            else if (timeRemaining <= 30f)
            {
                timerText.color = warningColor;
            }

            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        EndGame();
    }

    void EndGame()
    {
        if (gameManager != null)
        {
            timerText.gameObject.SetActive(false);
            gameManager.SetGameOver(true);
        }
    }
}
