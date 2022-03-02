using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxhealthBuff : MonoBehaviour
{
    public int maxHealthToAdd;
    private int currentMaxHealth;
    private int newMaxHealth;
    public  string onScreenMessage;
    private bool collected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !collected)
        {
            currentMaxHealth = PlayerHealthController.instance.maximumHealth;
            newMaxHealth = currentMaxHealth + maxHealthToAdd;
            // remove the on screen message from having a parent. So that we can destroy it after a certain amount of time
            onScreenMessage = "Your MAXIMUM Health has been increased! You now take less damage";
            PlayerHealthController.instance.currentHealth = newMaxHealth;
            PlayerHealthController.instance.maximumHealth = newMaxHealth;
            UIController.instance.healthSlider.maxValue = newMaxHealth;
            PlayerHealthController.instance.UpdateHealthBarText();
            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);
        }
    }
}
