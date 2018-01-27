using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public int maxSimultaneousSfxs;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    // SFX:
    public AudioClip test;
    public AudioClip[] footsteps;
    public AudioClip fire;
    public AudioClip jump;
    public AudioClip crash;
    public AudioClip lose;

    // Music:
    public AudioClip mainTheme;

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
    void Start () {
		if (instance == null)
        {
            instance = this;
            sfxSources = new AudioSource[maxSimultaneousSfxs];
            for (int i = 0; i < sfxSources.Length; i++)
            {
                sfxSources[i] = Instantiate(sfxSource, Camera.main.transform);
            }
        }
        else
        {
            Destroy(this);
        }
	}

    // Update is called once per frame
    void Update() {
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
            case Sfx.Jump:
                PlaySfx(jump);
                break;
            case Sfx.Crash:
                PlaySfx(crash);
                break;
            case Sfx.Lose:
                PlaySfx(lose);
                break;
        }
    }

    public void PlayMusic(Music song)
    {

        switch (song)
        {
            case Music.MainTheme:
                PlayMusic(mainTheme);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource != clip)
        {
            musicSource.Stop();
            musicSource.clip = mainTheme;
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
        Jump,
        Crash,
        Lose
    }

    public enum Music
    {
        MainTheme
    }
}
