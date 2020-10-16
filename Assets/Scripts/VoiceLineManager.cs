using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    public VoiceLineTrigger[] children;
    void Start()
    {
        children = GetComponentsInChildren<VoiceLineTrigger>();
    }

    public void StopCurrentlyPlayingVoiceLines() 
    {
        foreach (VoiceLineTrigger trig in children)
        {
            if (trig.isCurrenltyPlaying) {
                Debug.Log(trig.voiceLine.name + " IS CURRENTLY PLAYING  AT: " + Time.time);
                trig.StopVoiceLine();
            }
        }
    }
}
