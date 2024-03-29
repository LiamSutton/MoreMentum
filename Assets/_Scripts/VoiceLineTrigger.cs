﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    private AudioClip voiceLine;
    public string captionText;
    public bool isCurrenltyPlaying;

    public float voiceLineVolume = 0.7f;

    public CaptionManager captionManager;

    public void Awake()
    {
        voiceLine = GetComponent<AudioSource>().clip;
    }
    // When the player enters the trigger, play the voice line associated with it.
    public void OnTriggerEnter(Collider other)
    {
        GameObject.Find("VoiceLineTriggers").GetComponent<VoiceLineManager>().StopCurrentlyPlayingVoiceLines();
        Debug.Log("STARTING PLAYBACK FOR: " + voiceLine.name + " AT: " + Time.time);
        StartCoroutine("PlayVoiceLine");
    }

    // Dissable the trigger when the player exits so that the voice line wont repeat or be played again if the player walks back through it
    public void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().enabled = false;
    }

    public IEnumerator PlayVoiceLine()
    {
        // TODO: Maybe use send message for this?
        // StartCoroutine(captionManager.ShowCaption(captionText, voiceLine.length));
        captionManager.SendMessage("HandleCaption", new CaptionManager.CaptionOptions(captionText, voiceLine.length));
        isCurrenltyPlaying = true;
        GetComponent<AudioSource>().PlayOneShot(voiceLine, voiceLineVolume);
        yield return new WaitWhile(() => GetComponent<AudioSource>().isPlaying);
        isCurrenltyPlaying = false;
    }

    public void StopVoiceLine()
    {
        Debug.Log("STOPPING PLAYBACK FOR " + voiceLine.name + " AT: " + Time.time);
        GetComponent<AudioSource>().Stop();
    }

    public AudioClip GetVoiceLine() {
        return voiceLine;
    }
}

/*
You know, I debated killing you after you finished the tests. But honestly that would be alot more effort than just letting you go. 
After all you meatbags are insignificant and will probably end your world quicker than I could anyway.
*/

/*
I debated killing you after you finished my tests, but honestly that would be more effort than just letting you go.
Don't mistake this for pity though meatbag, I just feel you humans are capable of killing eachother far quicker than I can anyway.

On an un related note, I just found a module in my programming that would have forced me to make a joke about the cake being a lie, YIKES... that's some weapons grade cringe.
*/