using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayFootsteps() {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
        }
        
    }
}
