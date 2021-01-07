using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float newVolume)
    {
        mixer.SetFloat("MasterVolume", newVolume);
    }
}
