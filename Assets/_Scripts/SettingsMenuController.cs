using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterVolume(float newVolume)
    {
        mixer.SetFloat("MasterVolume", newVolume);
    }

    public void SetSfxVolume(float newVolume) {
        mixer.SetFloat("SFXVolume", newVolume);
    }

    public void SetVoiceVolume(float newVolume) {
        mixer.SetFloat("VoiceVolume", newVolume);
    }

    public void SetMusicVolume(float newVolume) {
        mixer.SetFloat("MusicVolume", newVolume);
    }
}
