﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public int maxSimultaneousSfxs;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    // SFX:
    public AudioClip[] footsteps;
    public AudioClip test;
    public AudioClip fire;
    public AudioClip roar;
    public AudioClip jumpStart;
    public AudioClip jumpEnd;
    public AudioClip crash;
    public AudioClip wing;
    public AudioClip[] bullets;
    public AudioClip cog;
    public AudioClip shoot;
    public AudioClip gameOver;

    // Music:
    public AudioClip mainTheme;
    public AudioClip menuTheme;

    private AudioSource[] sfxSources;

    private static Audio instance;

    public static Audio GetInstance()
    {
        return instance;
    }

    public static void Play(Music song)
    {
        if (GetInstance())
        {
            GetInstance().PlayMusic(song);
        }
    }

    public static void Play(Sfx sfx)
    {
        if (GetInstance())
        {
            GetInstance().PlaySfx(sfx);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
		if (instance == null)
        {
            instance = this;
            sfxSources = new AudioSource[maxSimultaneousSfxs];
            for (int i = 0; i < sfxSources.Length; i++)
            {
                sfxSources[i] = Instantiate(sfxSource, transform);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Audio.Play(Sfx.Test);
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        switch (sfx)
        {
            case Sfx.Test:
                PlaySfx(test);
                break;
            case Sfx.Footstep:
                PlaySfx(footsteps, 0.15f);
                break;
            case Sfx.Fire:
                PlaySfx(fire);
                break;
            case Sfx.Roar:
                PlaySfx(roar);
                break;
            case Sfx.Jump:
                PlaySfx(jumpStart);
                break;
            case Sfx.Land:
                PlaySfx(jumpEnd);
                break;
            case Sfx.Crash:
                PlaySfx(crash);
                break;
            case Sfx.Lose:
                PlaySfx(gameOver);
                break;
            case Sfx.Wing:
                PlaySfx(wing);
                break;
            case Sfx.Bullet:
                PlaySfx(bullets);
                break;
            case Sfx.Cog:
                PlaySfx(cog);
                break;
            case Sfx.Shoot:
                PlaySfx(shoot);
                break;
            default:
                Debug.LogWarning("Unknown SFX: " + sfx);
                break;
        }
    }

    public void PlayMusic(Music song)
    {
        switch (song)
        {
            case Music.MenuTheme:
                PlayMusic(menuTheme);
                break;
            case Music.MainTheme:
                PlayMusic(mainTheme);
                break;
            default:
                Debug.LogWarning("Unknown song: " + song);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    private void PlaySfx(AudioClip[] clips, float volumeScale = 1f)
    {
        PlaySfx(clips[UnityEngine.Random.Range(0, clips.Length)], volumeScale);
    }

    private void PlaySfx(AudioClip clip, float volumeScale = 1f)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clip, volumeScale);
                return;
            }
        }
        // if all are busy...
        sfxSources[0].PlayOneShot(clip, volumeScale);
    }

    public enum Sfx
    {
        Test,
        Footstep,
        Fire,
        Roar,
        Jump,
        Land,
        Crash,
        Lose,
        Wing,
        Bullet,
        Cog,
        Shoot
    }

    public enum Music
    {
        MenuTheme,
        MainTheme
    }
}
