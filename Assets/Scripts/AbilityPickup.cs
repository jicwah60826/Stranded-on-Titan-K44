using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public bool useGunsAbility, runAbility, jumpAbility, doubleJumpAbility, flashLightAbility, boosterBootsAbility;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
        }
    }
}
