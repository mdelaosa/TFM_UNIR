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

    [Header("Audio Clips")]
    [SerializeField] private AudioClip finishedAudioClip;  
    [SerializeField] private AudioClip winAudioClip;       
    [SerializeField] private AudioClip loseAudioClip;      

    private AudioSource audioSource;                      
    private float finishedMessageDuration = 2f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);
        if (finishedText != null) finishedText.SetActive(false);
        if (bluerBackground != null) bluerBackground.SetActive(false);
    }

    public void ShowFinishedMessageAndBackground(bool hasWon, int points)
    {
        StartCoroutine(ShowFinishedAnimation(hasWon, points));
    }

    private IEnumerator ShowFinishedAnimation(bool hasWon, int points)
    {
        if (finishedText != null && bluerBackground != null)
        {
            finishedText.SetActive(true);
            bluerBackground.SetActive(true);
        }
        else
        {
            Debug.LogError("ShowFinishedAnimation: El finishedText o bluerBackground no están asignados correctamente.");
        }

        if (audioSource != null && finishedAudioClip != null)
        {
            audioSource.clip = finishedAudioClip;
            audioSource.Play();
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

        yield return new WaitForSeconds(0.5f); 

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (finishedText != null && bluerBackground != null)
        {
            finishedText.SetActive(false);
            bluerBackground.SetActive(false);
        }

        ShowGameOverScreen(hasWon, points);
    }

    private void ShowGameOverScreen(bool hasWon, int points)
    {
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
            if (winScreen != null)
            {
                winScreen.SetActive(true);
                PlaySound(winAudioClip);  
            }
        }
        else
        {
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
                PlaySound(loseAudioClip);  
            }
        }
    }

    public void PlayerWon(int points)
    {
        ShowFinishedMessageAndBackground(true, points);
    }

    public void PlayerLost(int points)
    {
        ShowFinishedMessageAndBackground(false, points);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
