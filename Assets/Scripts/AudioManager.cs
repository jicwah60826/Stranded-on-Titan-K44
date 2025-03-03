using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource bgMusic, bgAmbience, levelVictory;

    public AudioSource[] soundEffects;

    public AudioSource[] playerShotSFX;

    private void Awake()
    {
        instance = this;

        GetAudioPrefs();
    }

    private static void GetAudioPrefs()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            float masterVolAmount = PlayerPrefs.GetFloat("MusicVolume");
            UIController.instance.theMixer.SetFloat("MasterVolumeParam", masterVolAmount);
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1.0f);
            //sliderSFX.value = 1.0f;
        }
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

    public void PlayerShotSFX()
    {
        int playerShotSFXLength = playerShotSFX.Length;
        int clipToPlay = Random.Range(0,playerShotSFXLength);
        Debug.Log("playersfx clip to play: " + clipToPlay);
        playerShotSFX[clipToPlay].Play();
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
