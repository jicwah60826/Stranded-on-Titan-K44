using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Slider healthSlider, staminaSlider, airSlider;
    public Text healthText, ammoText, airText;

    public Image damageEffect, blackScreen;
    public float damageAlphaTarget;
    public float damageFadeSpeed, blackScreenFadeSpeed;

    public GameObject pauseScreenOverlay, pauseMenuMain, audioSubMenu, controlsSubMenu, confirmQuit, crosshairs, ammoCounter, centerDot, onScreenTextCanvas;

    public AudioMixer theMixer;
    public TMP_Text masterVolLabel, musicVolLabel, sfxVolLabel, ambientSFXVolLabel, onScreenTextLowerThird;
    public Slider masterVolSlider, musicVolSlider, sfxVolSlider, ambientSFXVolSlider;

    private void Awake()
    {
        instance = this; // allow this script to be accessed anywhere

        setMasterVolume();
        setMusicVolume();
        setSFXVolume();
        setAmbienceVolume();
    }

    private void Start()
    {
        //deactivate pause overlay at game start
        pauseScreenOverlay.gameObject.SetActive(false);
        //Debug.Log("UIController: pauseScreenOverlay DISABLED at start");

        // ensure we de-activate the on screen lower third text
        onScreenTextCanvas.gameObject.SetActive(false);

        //activate black fader for fade in effect
        blackScreen.gameObject.SetActive(true); //enable black on start

        //Disable / Enable Air UI based on useAir
        if (PlayerHealthController.instance.useAir == false)
        {
            airText.gameObject.SetActive(false);
            airSlider.gameObject.SetActive(false);
        }
        else
        {
            airText.gameObject.SetActive(true);
            airSlider.gameObject.SetActive(true);
        }


    }

    private void Update()
    {
        if (damageEffect.color.a > 0)
        {
            // Fade the damage effect alpha out. Using a Mathf that uses the damage fade speed
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }

        //Fade to / from black
        if (!GameManager.instance.levelEnding == true)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFadeSpeed * Time.deltaTime));
        }
        else
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, blackScreenFadeSpeed * Time.deltaTime));
        }
    }
    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlphaTarget);
    }


    public void setMasterVolume()
    {
        masterVolLabel.text = Mathf.RoundToInt(masterVolSlider.value).ToString() + "%";
        theMixer.SetFloat("MasterVolumeParam", Mathf.Log10(masterVolSlider.value) * 20);
    }

    public void setMusicVolume()
    {
        musicVolLabel.text = Mathf.RoundToInt(musicVolSlider.value).ToString() + "%";
        theMixer.SetFloat("MusicVolumeParam", Mathf.Log10(musicVolSlider.value) * 20);
    }

    public void setSFXVolume()
    {
        sfxVolLabel.text = Mathf.RoundToInt(sfxVolSlider.value).ToString() + "%";
        theMixer.SetFloat("SFXVolumeParam", Mathf.Log10(sfxVolSlider.value) * 20);
    }

    public void setAmbienceVolume()
    {
        ambientSFXVolLabel.text = Mathf.RoundToInt(ambientSFXVolSlider.value).ToString() + "%";
        theMixer.SetFloat("AmbienceVolumeParam", Mathf.Log10(ambientSFXVolSlider.value) * 20);
    }
}