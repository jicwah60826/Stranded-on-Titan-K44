using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPickup : MonoBehaviour
{
    public bool useGunsAbility, runAbility, jumpAbility, doubleJumpAbility, flashLightAbility, boosterBootsAbility, wayPointsAbility, maxHealthIncrease;
    public string onScreenMessage, hasWayPointPerk;
    public TMP_Text onScreenMessageText;
    public float textOnScreenTime;
    public float textFadeTime;

    public int maxHealthToAdd;
    private int currentMaxHealth;
    private int newMaxHealth;
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !collected)
        {

            //Show Text
            onScreenMessageText.gameObject.SetActive(true);

            // Remove Ability Canvas as a child object
            onScreenMessageText.transform.parent.SetParent(null);

            // Set Text
            onScreenMessageText.text = onScreenMessage;


            //  USE GUNS ABILITY
            if (useGunsAbility)
            {
                PlayerController.instance.useGunsAbility = true;
            }

            // RUN ABILITY

            if (runAbility)
            {
                PlayerController.instance.runAbility = true;
            }

            // JUMP ABILITY

            if (jumpAbility)
            {
                PlayerController.instance.jumpAbility = true;
            }

            // DOUBLE JUMP ABILITY 
            if (doubleJumpAbility)
            {
                PlayerController.instance.doubleJumpAbility = true;
            }

            // USE FLASHLIGHT ABILITY 
            if (flashLightAbility)
            {
                PlayerController.instance.flashLightAbility = true;
            }

            // BOOSTER BOOTS ABILITY 
            if (boosterBootsAbility)
            {
                PlayerController.instance.boosterBootsAbility = true;
            }

            // Max Health Increase 
            if (maxHealthIncrease)
            {
                currentMaxHealth = PlayerHealthController.instance.maximumHealth;
                newMaxHealth = currentMaxHealth + maxHealthToAdd;
                PlayerHealthController.instance.currentHealth = newMaxHealth;
                PlayerHealthController.instance.maximumHealth = newMaxHealth;
                UIController.instance.healthSlider.maxValue = newMaxHealth;
                PlayerHealthController.instance.UpdateHealthBarText();
            }

            // WAYPOINTS ABILITY 
            if (wayPointsAbility)
            {
                PlayerController.instance.wayPointsAbility = true;
                // Update Player Prefs
                hasWayPointPerk = "true";
                PlayerPrefs.SetString("hasWayPointPerk", hasWayPointPerk);
                Debug.Log("hasWayPointPerk has been set to " + hasWayPointPerk);
                // Enable Waypoint system in game
                GameManager.instance.wayPointsEnabled = true;
            }


            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);

            // Destroy Text after wait time
            Destroy(onScreenMessageText.transform.parent.gameObject, textOnScreenTime);
        }
    }
}
