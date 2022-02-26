using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public string theGun;

    private bool collected;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !collected)
        {
            //collect gun and add to list
            PlayerController.instance.AddGun(theGun); // call the AddGun function on the PlayerController
            AudioManager.instance.PlaySFX(4); // play sfx element from audio manager SFX list
            AudioManager.instance.PlaySFX(10); // play sfx element from audio manager SFX list
            Destroy(gameObject);
            collected = true;
            PlayerController.instance.gunHolstered = false;
            PlayerController.instance.useGunsAbility = true;
        }
    }

}
