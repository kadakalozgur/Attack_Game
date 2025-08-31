using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public GameObject healthBarCanvas;
    public GameObject potionNumberCanvas;
    public GameObject strongAttackCanvas;
    public GameObject gameOverCanvas;
    public GameObject playerScoreCanvas;
    public PlayerCamera playerCamera;
    public AudioSource audioSource;
    public AudioSource backgroundMusic;
    public AudioClip gameOverSound;
    public PlayerScore playerScore;
    public TMP_Text scoreText;

    public bool endGame = false;

    public void setup()
    {

        Time.timeScale = 0f;

        endGame = true;

        audioSource.PlayOneShot(gameOverSound, 0.3f);

        gameOverCanvas.SetActive(true);
        healthBarCanvas.SetActive(false);
        potionNumberCanvas.SetActive(false);
        strongAttackCanvas.SetActive(false);
        playerScoreCanvas.SetActive(false);

        scoreText.text = "Score : " + playerScore.score;

        playerCamera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        backgroundMusic.Stop();

    }

    public void restartButton()
    {

        Time.timeScale = 1f;

        endGame = false;

        SceneManager.LoadScene("SampleScene");

    }

    public void quitButton()
    {

        Application.Quit();

    }
}
