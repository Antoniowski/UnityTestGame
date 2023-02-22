using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAudioHandler : MonoBehaviour
{
    public Sound[] sounds;
    AudioSource audioSource;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void SetUpSounds()
    {
        foreach (Sound s in sounds)
        {
            
        }
    }

    public void PlayTargetSoundAtPoint(string clipName, Vector3 point)
    {
        Sound toPlay = Array.Find(sounds, s => s.name == clipName);
        AudioSource.PlayClipAtPoint(toPlay.clip, point);
    }

    public void PlayTargetSound(string clipName)
    {
        Sound s = Array.Find(sounds, s => s.name == clipName);
        audioSource.clip = s.clip;
        audioSource.volume = s.volume*audioManager.SFXVolume;
        audioSource.loop = false;
        audioSource.Play();

    }
}
