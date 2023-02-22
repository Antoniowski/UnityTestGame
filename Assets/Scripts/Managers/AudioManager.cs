using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    //SIngleton
    public static AudioManager instance;

    //Array da suonare
    public Sound[] suonds;

    public float musicVolume;
    public float SFXVolume;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SetUpSounds();
    }
    // Start is called before the first frame update
    void Start()
    {
        Play("MusicDemo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpSounds()
    {
        foreach (Sound s in suonds)
        {
            s.source = GetComponentInChildren<AudioPlayer>().gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string clipName)
    {
        Sound s = Array.Find(suonds, sound => sound.name == clipName);
        s.source.Play();
    }
}
