using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverlayUI;
    public GameObject player;



    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        pauseMenuUI.SetActive(true);
        gameOverlayUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        pauseMenuUI.SetActive(false);
        gameOverlayUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
