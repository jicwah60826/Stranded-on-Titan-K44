using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Slider healthSlider, staminaSlider;
    public Text healthText;
    public Text ammoText;

    public Image damageEffect, blackScreen;
    public float damageAlphaTarget;
    public float damageFadeSpeed, blackScreenFadeSpeed;

    public GameObject pauseScreenOverlay, pauseMenuMain, audioSubMenu, controlsSubMenu;

    private void Awake()
    {
        instance = this; // allow this script to be accessed anywhere
        blackScreen.gameObject.SetActive(true); //enable black on awake
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
        else{
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, blackScreenFadeSpeed * Time.deltaTime));
        }
    }
    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlphaTarget);
    }
}
