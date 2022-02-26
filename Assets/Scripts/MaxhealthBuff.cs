using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxhealthBuff : MonoBehaviour
{
    public int maxHealthMultiplier;
    private int currentMaxHealth;
    private int newMaxHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentMaxHealth = PlayerHealthController.instance.maximumHealth;
            newMaxHealth = currentMaxHealth * maxHealthMultiplier;

            PlayerHealthController.instance.currentHealth = newMaxHealth;
            PlayerHealthController.instance.maximumHealth = newMaxHealth;
            UIController.instance.healthSlider.maxValue = newMaxHealth;
            PlayerHealthController.instance.UpdateHealthBarText();
            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);
        }
    }
}
