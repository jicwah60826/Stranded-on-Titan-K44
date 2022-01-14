using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public Text ammoText;

    public Image damageEffect;
    public float damageAlphaTarget;
    public float damageFadeSpeed;

    public GameObject pauseScreen;

    private void Awake()
    {
        instance = this; // allow this script to be accessed anywhere
    }

    private void Update()
    {
        if (damageEffect.color.a > 0)
        {
            // Fade the damage effect alpha out. Using a Mathf that uses the damage fade speed
            damageEffect.color  = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }
    }
    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlphaTarget);
    }
}
