using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioManager : MonoBehaviour
{
    
    AudioManager masterAudioManager;
   
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            masterAudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }catch
        {
            Debug.LogError("Manager not Found");
        }
        audioSource = GetComponent<AudioSource>();
        if(masterAudioManager != null)
        {
            audioSource.volume = masterAudioManager.SFXVolume;
        }
        else
        {
            
        }
         
    }

    void Start()
    {
        if(audioSource)
            audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Aggiorna Volume
        if(masterAudioManager != null)
        {
            if(masterAudioManager.SFXVolume != audioSource.volume)
                audioSource.volume = masterAudioManager.SFXVolume;
        }
    }
}
