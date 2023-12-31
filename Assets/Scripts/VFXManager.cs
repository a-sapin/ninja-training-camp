using System;
using UnityEngine;
using UnityEngine.Audio;

public class VFXManager : MonoBehaviour
{
    public bool isBruh = false;
    public AudioSource bruh;
    public SoundEffect[] sounds;
    //public SoundEffect[] soundsMovements;
    public static VFXManager instance;
    
    public AudioMixerGroup mixer;
    // Use this for initialization
    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundEffect s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
    }

    public void Play(string name)
    {
        if (isBruh)
        {
            bruh.Play();
            return;
        }
       SoundEffect s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else if (!s.source.isPlaying)
        {
            s.source.Play();
        }
        else
        {
            s.source.Stop();
        }
    }

    public void Stop(string name)
    {
        SoundEffect s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void Pause(string name)
    {
        SoundEffect s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void StopAll()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            SoundEffect s = sounds[i];
            s.source.Stop();
        }
    }

    public void PauseAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            SoundEffect s = sounds[i];
            s.source.Pause();
        }
    }

    public void MuteAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            SoundEffect s = sounds[i];
            s.source.mute = true;
        }
    }

    public void DeMuteAll()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            SoundEffect s = sounds[i];
            s.source.mute = false;
        }
    }
}
