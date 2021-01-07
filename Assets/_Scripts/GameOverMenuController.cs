using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    AudioSource audiosource;
    AudioClip buttonClickSound;

    public GameObject suprise;

    public void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audiosource = GetComponent<AudioSource>();
        buttonClickSound = audiosource.clip;
    }
    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GiveFirst() {
        suprise.SetActive(true);
    }

    public void PlayButtonClickSound() {
        audiosource.PlayOneShot(buttonClickSound);
    }
}
