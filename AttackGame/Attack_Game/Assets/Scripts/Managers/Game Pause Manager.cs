using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseManager : MonoBehaviour
{

    public GameObject healthBarCanvas;
    public GameObject potionNumberCanvas;
    public GameObject gamePauseCanvas;
    public PlayerCamera playerCamera;
    public GameOverManager gameOverManager;
    public AudioSource backgroundMusic;

    private bool isPause = false;


    private void Start()
    {

        gamePauseCanvas.SetActive(false);

        backgroundMusic.volume = 0.3f;

    }
    void Update()
    {

        if (!gameOverManager.endGame)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (isPause)
                {

                    ContinueGame();

                }

                else
                {

                    PauseGame();

                }

            }
        }
    }

    public void ContinueGame()
    {

        Time.timeScale = 1f;

        isPause = false;

        gamePauseCanvas.SetActive(false);
        healthBarCanvas.SetActive(true);
        potionNumberCanvas.SetActive(true);

        playerCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        backgroundMusic.UnPause();

    }

    public void PauseGame()
    {

        Time.timeScale = 0f;

        isPause = true;

        gamePauseCanvas.SetActive(true);
        healthBarCanvas.SetActive(false);
        potionNumberCanvas.SetActive(false);

        playerCamera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        backgroundMusic.Pause();

    }

    public void quitGame()
    {

        Application.Quit();

    }
}
