using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip buttonClickSound;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        buttonClickSound = audioSource.clip;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
