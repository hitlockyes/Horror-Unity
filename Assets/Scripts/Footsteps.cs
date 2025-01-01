using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public GameObject footstep;
    public AudioSource footstepAudioSource; 
    public bool isSprinting = false;

    void Start()
    {
        footstep.SetActive(false);
        footstepAudioSource = footstep.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            isSprinting = true;
            footstepAudioSource.pitch = 2.0f; 
        }
        else
        {
            isSprinting = false;
            footstepAudioSource.pitch = 1.0f; 
        }

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            footsteps();
        }
        else
        {
            StopFootsteps();
        }
    }

    void footsteps()
    {
        footstep.SetActive(true);
        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
        footstepAudioSource.Stop();
    }
}

