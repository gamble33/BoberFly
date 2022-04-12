using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;
    
    [SerializeField]
    private AudioType[] audios;

    private Dictionary<Sound, AudioSource> _playingSounds = new Dictionary<Sound, AudioSource>();
    
    public enum Sound
    {
        MachineMove,
    }

    public enum Music
    {
    }

    /// <summary>
    /// Play a sound and loop it until StopSound(sound) is called.
    /// If a sound that is already playing, is started, it will return.
    /// </summary>
    /// <param name="sound"></param>
    public void StartSound(Sound sound)
    {
        if (_playingSounds.ContainsKey(sound)) return;
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        Audio audio = GetAudio(sound);
        audioSource.clip = audio.clip;
        audioSource.loop = audio.loop;
        _playingSounds.Add(sound, audioSource);
        audioSource.Play();
    }

    /// <summary>
    /// Stops a sound from playing.
    /// </summary>
    /// <param name="sound"></param>
    public void StopSound(Sound sound)
    {
        if (!_playingSounds.ContainsKey(sound)) return;
        _playingSounds[sound].Stop();
        Destroy(_playingSounds[sound]);
        _playingSounds.Remove(sound);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private Audio GetAudio(Sound sound)
    {
        foreach (AudioType audioType in audios)
        {
            if (audioType.sound == sound)
            {
                return audioType.audios[Random.Range(0, audioType.audios.Length)];
            }
        }
        Debug.LogError($"Couldn't find sound ${sound}");
        return null;
    }
}