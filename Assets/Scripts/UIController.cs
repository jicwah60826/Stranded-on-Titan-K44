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

    public GameObject inGameOverlays, pauseScreenOverlay, pauseMenuMain, audioSubMenu, controlsSubMenu, confirmQuit, crosshairs, ammoCounter, centerDot, onScreenTextCanvas, staminaBar, scrapMetalOverlay;

    public AudioMixer theMixer;
    public TMP_Text masterVolLabel, musicVolLabel, sfxVolLabel, ambientSFXVolLabel, onScreenTextLowerThird, scrapMetalCounterText;
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

        // enable In Game Overlays by default
        inGameOverlays.gameObject.SetActive(true);

        //deactivate pause overlay at game start
        pauseScreenOverlay.gameObject.SetActive(false);
        //Debug.Log("UIController: pauseScreenOverlay DISABLED at start");

        // enable the pause menu main by default (child of pause overlay)
        pauseMenuMain.gameObject.SetActive(true);

        // ensure we de-activate the on screen lower third text
        onScreenTextCanvas.gameObject.SetActive(false);

        //activate black fader for fade in effect
        blackScreen.gameObject.SetActive(true); //enable black on start

        // hide sub-menus by default
        controlsSubMenu.gameObject.SetActive(false);
        audioSubMenu.gameObject.SetActive(false);


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


        // Update Loot Count UI
        int lootCount = PlayerPrefs.GetInt("MetalCount", 0);
        scrapMetalCounterText.text = lootCount.ToString();

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
        //masterVolLabel.text = Mathf.RoundToInt(masterVolSlider.value).ToString() + "%";
        theMixer.SetFloat("MasterVolumeParam", Mathf.Log10(masterVolSlider.value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", Mathf.Log10(masterVolSlider.value) * 20);
    }

    public void setMusicVolume()
    {
        //musicVolLabel.text = Mathf.RoundToInt(musicVolSlider.value).ToString() + "%";
        theMixer.SetFloat("MusicVolumeParam", Mathf.Log10(musicVolSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(musicVolSlider.value) * 20);
    }

    public void setSFXVolume()
    {
        //sfxVolLabel.text = Mathf.RoundToInt(sfxVolSlider.value).ToString() + "%";
        theMixer.SetFloat("SFXVolumeParam", Mathf.Log10(sfxVolSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(musicVolSlider.value) * 20);
    }

    public void setAmbienceVolume()
    {
        //ambientSFXVolLabel.text = Mathf.RoundToInt(ambientSFXVolSlider.value).ToString() + "%";
        theMixer.SetFloat("AmbienceVolumeParam", Mathf.Log10(ambientSFXVolSlider.value) * 20);
        PlayerPrefs.SetFloat("AmbientSFXVolume", Mathf.Log10(musicVolSlider.value) * 20);
    }
}