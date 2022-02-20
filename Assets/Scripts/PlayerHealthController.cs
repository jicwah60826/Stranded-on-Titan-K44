using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    [Header("Player Health")]
    [Space]
    [Tooltip("Player maximum health in integers")]
    public int maximumHealth;
    [Tooltip("Player current health. This is what is reduced with damage or increased with a pickup.")]
    public int currentHealth;
    [Space]
    [Header("Grace Period")]
    [Tooltip("The amount of time the player goes without damager after being shot.")]
    public float invicibleLength;
    private float invincibleCounter;
    public bool useInvicDelay;
    public bool receiveDamage;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        receiveDamage = true;
        currentHealth = maximumHealth;
        UIController.instance.healthSlider.maxValue = maximumHealth; //set slider max value
        UpdateHealthBarText();

    }

    private void Update()
    {
        InvincibleCounter();
    }

    private void InvincibleCounter()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; // begin invincible countdown once player is first shot
        }
    }

    public void DamagePlayer(int damageAmount)
    {

        if (invincibleCounter <= 0 || useInvicDelay == false)
        {

            // if level is NOT ending - then we can allow the below
            if (!GameManager.instance.levelEnding)
            {
                if (receiveDamage == true)
                {
                    currentHealth -= damageAmount; // de-iterate player health
                }



                UIController.instance.ShowDamage(); // show damage
                AudioManager.instance.PlaySFX(7); // play sfx element from audio manager SFX list

                if (currentHealth <= 0)
                {
                    // Player dead
                    //Debug.Log("Player has been killed");
                    gameObject.SetActive(false); // disable player controls / movement
                    currentHealth = 0;  // reset health to 0 so healthbar display never shows a negative #
                    GameManager.instance.PlayerDied(); // call player function from GameManager
                    AudioManager.instance.StopBGM(); // stop the background music
                    AudioManager.instance.PlaySFX(6); // play sfx element from audio manager SFX list
                }
            }


        }
        invincibleCounter = invicibleLength;
        UpdateHealthBarText();
    }

    public void UpdateHealthBarText()
    {
        UIController.instance.healthSlider.value = currentHealth; // initialize slider value to current health
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maximumHealth;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }

        //update slider and text
        UpdateHealthBarText();

    }
}
