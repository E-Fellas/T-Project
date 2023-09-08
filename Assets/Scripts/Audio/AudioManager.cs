using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public string bgm;
    public Audio[] audios;
    public AudioMixerGroup output;
    [HideInInspector]
    public static AudioManager instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (this.bgm != instancia.bgm || !Array.Find(instancia.audios, audio => audio.name == bgm).source.isPlaying)
        {
            Destroy(instancia.gameObject);
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.loop = a.loop;
            a.source.outputAudioMixerGroup = output;
        }
    }

    private void Start()
    {
        if(!(bgm == null) || !(bgm == ""))
            Play(bgm);
    }

    public void Play(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        if (a == null)
        {
            Debug.LogWarning("Audio nao encontrado");
            return;
        }
        a.source.Play();
    }

    public void Stop(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        if (a == null)
        {
            Debug.LogWarning("Audio nao encontrado");
            return;
        }
        a.source.Stop();
    }

    void Update()
    {
        
    }
}