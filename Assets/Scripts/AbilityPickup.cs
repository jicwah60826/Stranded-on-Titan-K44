using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPickup : MonoBehaviour
{
    public bool useGunsAbility, runAbility, jumpAbility, doubleJumpAbility, flashLightAbility, boosterBootsAbility, wayPointsAbility, maxHealthIncrease;
    public string onScreenMessage;
    private string hasWayPointsAbility;
    public float textOnScreenTime;

    public int maxHealthToAdd;
    private int currentMaxHealth;
    private int newMaxHealth;
    private bool collected = false;

    public GameObject thePickupModel;

    public Collider theCollider;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !collected)
        {

            // Invoke co routine
            StartCoroutine(OnScreenTextController());

            collected = true;

            // disable the collider
            theCollider.enabled = false;

            // deactivate the pickup model
            thePickupModel.SetActive(false);

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
                hasWayPointsAbility = "true";
                PlayerPrefs.SetString("wayPointsAbility", hasWayPointsAbility);
                //Debug.Log("wayPointsAbility has been set to " + hasWayPointsAbility);
                // Enable Waypoint system in game
                GameManager.instance.wayPointsEnabled = true;
            }

            AudioManager.instance.PlaySFX(17); // play sfx element from audio manager SFX list
        }
    }

    public IEnumerator OnScreenTextController()
    {

        // Set On Screen Message UI text
        UIController.instance.onScreenTextLowerThird.text = onScreenMessage;

        // Enable the on screen message UI element
        UIController.instance.onScreenTextCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(textOnScreenTime);

        // Disable the on screen text after a certain amount of time
        UIController.instance.onScreenTextCanvas.SetActive(false);

        // Clear the text from the on screen text (ready for future use)
        UIController.instance.onScreenTextLowerThird.text = "";

        // Destroy this game object a short time after the text has been disabled
        Destroy(gameObject, textOnScreenTime + .5f);

        yield return null;
    }

}
