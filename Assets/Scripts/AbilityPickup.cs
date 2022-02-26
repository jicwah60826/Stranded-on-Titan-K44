using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityPickup : MonoBehaviour
{
    public bool useGunsAbility, runAbility, jumpAbility, doubleJumpAbility, flashLightAbility, boosterBootsAbility;
    public string onScreenMessage;
    public TMP_Text onScreenMessageText;
    public float textOnScreenTime;
    public float textFadeTime;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
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

            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);

            // Destroy Text after wait time
            Destroy(onScreenMessageText.transform.parent.gameObject, textOnScreenTime);
        }
    }
}
