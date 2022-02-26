using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    [Header("Player Health")]
    [Space]
    [Tooltip("Player maximum health and max air in integers")]
    public int maximumHealth;
    public float maximumAir;
    [Tooltip("Player current health and current air. This is what is reduced with damage or increased with a pickup.")]
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public float currentAir;
    [Space]
    [Header("Grace Period")]
    [Tooltip("The amount of time the player goes without damager after being shot.")]
    public float invicibleLength;
    private float invincibleCounter;
    public bool useInvicDelay;
    public bool receiveDamage;
    private int playerPrefMaxHealth;
    public float airDepleteRate;
    public float suffocateBuffer;
    private float suffocateCounter;
    public int suffocationDamageAmount;
    public float suffocationDamageInterval;
    private float suffocateDamageIntervalTimer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        receiveDamage = true;
        currentHealth = maximumHealth;
        currentAir = maximumAir;
        suffocateCounter = suffocateBuffer;
        suffocateDamageIntervalTimer = suffocationDamageInterval;
        UIController.instance.healthSlider.maxValue = maximumHealth; //set slider max value

        UpdateHealthBarText();
        UpdateAirTankText();
        PlayerPrefHealthAmount();

    }

    private void Update()
    {
        InvincibleCounter();
        DepleteAir();
    }

    private void DepleteAir()
    {
        if (currentAir > 0)
        {
            currentAir -= Time.deltaTime / airDepleteRate;
            UpdateAirTankText();
        }
        else if (currentAir <= 0)
        {
            currentAir = 0;
        }


        if (currentAir <= 0)
        {
            suffocateCounter -= Time.deltaTime;

            if (suffocateCounter <= 0)
            {
                //begin counter that hurts playerevery X seconds
                suffocateDamageIntervalTimer -= Time.deltaTime;
                if (suffocateDamageIntervalTimer <= 0)
                {
                    suffocateDamageIntervalTimer = suffocationDamageInterval;
                    DamagePlayer(suffocationDamageAmount);
                }
            }
        }
        else
        {
            suffocateCounter = suffocateBuffer;

        }

        if (suffocateCounter <= 0)
        {
            // begin countdown clock to hurt player every 
        }
    }

    private void PlayerPrefHealthAmount()
    {

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

    public void UpdateAirTankText()
    {
        UIController.instance.airSlider.value = currentAir; // initialize slider value to current air
        //UIController.instance.airText.text = "AIR: " + currentAir;
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

    public void GiveAir(float airAmount)
    {
        currentAir += airAmount;

        if (currentAir > maximumAir)
        {
            currentAir = maximumAir;
        }

        //update air slider and text
        UpdateAirTankText();

    }

    public void IncreaseMaxHealth(int maxHealthIncreaseAmount)
    {
        Debug.Log("current MAX health is " + maximumHealth);
        Debug.Log(maxHealthIncreaseAmount + " will be added to max health amount");
    }
}
