using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public AudioClip voiceLine;

    // When the player enters the trigger, play the voice line associated with it.
    public void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(voiceLine);
    }

    // Dissable the trigger when the player exits so that the voice line wont repeat or be played again if the player walks back through it
    public void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().enabled = false;
    }
}
