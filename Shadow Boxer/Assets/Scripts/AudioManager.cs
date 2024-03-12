using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] punchSounds;
    [SerializeField] private AudioClip whooshSound;
    [SerializeField] private AudioClip spinSound;

    public void PlayPunch()
    {
        //play a random punch sound
        SFXSource.PlayOneShot(punchSounds[Random.Range(0, punchSounds.Length - 1)]);
    }

    public void PlayWhoosh()
    {
        //save pitch
        float original_pitch = SFXSource.pitch;

        //randomize pitch
        SFXSource.pitch = Random.Range(-1.1f, 2.1f);

        //play sound
        SFXSource.PlayOneShot(whooshSound); 

        //reset pitch  
        SFXSource.pitch = original_pitch;
    }

    public void PlaySpinSound()
    {
        //play sound
        SFXSource.PlayOneShot(spinSound); 
    }
}
