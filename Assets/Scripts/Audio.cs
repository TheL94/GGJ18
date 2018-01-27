using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip test, drums;
    public AudioClip mainTheme;

    private AudioSource[] sfxSources;

    private static Audio instance;    
    public static Audio GetInstance()
    {
        return instance;
    }

	// Use this for initialization
	void Start () {
		if (instance == null)
        {
            instance = this;
            sfxSources = new AudioSource[6];
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
            Play(Sfx.Test);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(Sfx.Drums);
        }
    }

    public void Play(Sfx sfx)
    {

        switch (sfx)
        {
            case Sfx.Test:
                PlaySfx(test);
                break;
            case Sfx.Drums:
                PlaySfx(drums);
                break;
        }
    }

    public void Play(Music song)
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

    private void PlaySfx(AudioClip clip)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clip);
                return;
            }
        }
        // if all are busy...
        sfxSources[0].PlayOneShot(clip);
    }

    public enum Sfx
    {
        Test,
        Drums
    }

    public enum Music
    {
        MainTheme
    }
}
