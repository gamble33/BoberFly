using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;
    
    [SerializeField]
    private Audio[] audios;
    private readonly Dictionary<Sound, List<AudioClip>> _audioClips = new Dictionary<Sound, List<AudioClip>>();
    
    public enum Sound
    {
        MachineMove,
    }

    public enum Music
    {
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        InitSounds();
    }

    private void InitSounds()
    {
        foreach (Audio audio in audios)
        {
            _audioClips.Add(audio.sound, );
        } 
    }

    /// <summary>
    /// Play a sound and loop it until StopSound(sound) is called.
    /// </summary>
    /// <param name="sound"></param>
    public void StartSound(Sound sound)
    {
    }

    /// <summary>
    /// Stops a sound from playing.
    /// </summary>
    /// <param name="sound"></param>
    public void StopSound(Sound sound)
    {
    }
}