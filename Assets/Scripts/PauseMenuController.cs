using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverlayUI;
    public GameObject player;



    void Start() {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    void Pause() {
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        pauseMenuUI.SetActive(true);
        gameOverlayUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void Resume() {
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        pauseMenuUI.SetActive(false);
        gameOverlayUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
