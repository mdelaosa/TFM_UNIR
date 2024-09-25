using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Finished UI Elements")]
    [SerializeField] private GameObject finishedText;
    [SerializeField] private GameObject bluerBackground;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI pointsTextGameOver;

    private float finishedMessageDuration = 2f;

    void Start()
    {
        Debug.Log("GameOverManager Start: Iniciando...");

        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);
        if (finishedText != null) finishedText.SetActive(false);
        if (bluerBackground != null) bluerBackground.SetActive(false);
    }

    public void ShowFinishedMessageAndBackground(bool hasWon, int points)
    {
        Debug.Log("ShowFinishedMessageAndBackground: Iniciando la animación del mensaje 'Finished'");

        StartCoroutine(ShowFinishedAnimation(hasWon, points));
    }

    private IEnumerator ShowFinishedAnimation(bool hasWon, int points)
    {
        Debug.Log("ShowFinishedAnimation: Activando el texto 'Finished' y el fondo borroso.");

        if (finishedText != null && bluerBackground != null)
        {
            finishedText.SetActive(true);
            bluerBackground.SetActive(true);
        }
        else
        {
            Debug.LogError("ShowFinishedAnimation: El finishedText o bluerBackground no están asignados correctamente.");
        }

        Vector3 originalPosition = finishedText.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < finishedMessageDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * 2f;
            float offsetY = Random.Range(-1f, 1f) * 2f;

            finishedText.transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        finishedText.transform.localPosition = originalPosition;

        yield return new WaitForSeconds(0.5f); // Esperar medio segundo antes de ocultar

        if (finishedText != null && bluerBackground != null)
        {
            finishedText.SetActive(false);
            bluerBackground.SetActive(false);
        }

        Debug.Log("ShowFinishedAnimation: Mostrando la pantalla de Game Over.");
        ShowGameOverScreen(hasWon, points);
    }

    private void ShowGameOverScreen(bool hasWon, int points)
    {
        Debug.Log("ShowGameOverScreen: Activando la pantalla de Game Over");

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        if (pointsTextGameOver != null)
        {
            pointsTextGameOver.text = "Final points: " + points;
        }

        if (hasWon)
        {
            Debug.Log("ShowGameOverScreen: Activando la pantalla de victoria.");
            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
        else
        {
            Debug.Log("ShowGameOverScreen: Activando la pantalla de derrota.");
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }
        }
    }

    public void PlayerWon(int points)
    {
        Debug.Log("PlayerWon: El jugador ha ganado con " + points + " puntos.");
        ShowFinishedMessageAndBackground(true, points);
    }

    public void PlayerLost(int points)
    {
        Debug.Log("PlayerLost: El jugador ha perdido con " + points + " puntos.");
        ShowFinishedMessageAndBackground(false, points);
    }
}
