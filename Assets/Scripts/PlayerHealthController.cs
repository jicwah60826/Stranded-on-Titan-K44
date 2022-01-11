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
    private float invicibleCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;

        UIController.instance.healthSlider.maxValue = maximumHealth; //set slider max value
        updateHealthBarText();

    }

    private void Update()
    {
        Debug.Log("invicibleCounter: " + invicibleCounter);
        Debug.Log("player health: " + currentHealth);

        if (invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime; // begin invincible countdown once player is first shot
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invicibleCounter <= 0)
        {
            currentHealth -= damageAmount; // de-iterate player health
            if (currentHealth <= 0)
            {
                // Player dead
                Debug.Log("Player has been killed");
                gameObject.SetActive(false); // disable player controls / movement

                currentHealth = 0;  // reset health to 0 so healthbar display never shows a negative #

                GameManager.instance.PlayerDied(); // call player function from GameManager


            }
        }
        invicibleCounter = invicibleLength;
        updateHealthBarText();
    }

    private void updateHealthBarText()
    {
        UIController.instance.healthSlider.value = currentHealth; // initialize slider value to current health
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maximumHealth;
    }
}
