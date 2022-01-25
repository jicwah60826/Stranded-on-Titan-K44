using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource bgMusic, bgAmbience, levelVictory;

    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    public void StopBGM()
    {
        bgMusic.Stop();
    }

    public void LevelVictory()
    {
        StopBGM();
        levelVictory.Play();
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
