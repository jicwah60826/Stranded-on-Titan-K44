using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    private bool collected;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && !collected)
        {
            //give ammo to player
            PlayerController.instance.activeGun.GetAmmo(); // call the GetAmmo function on the GunController script that we have tied to the PlayerController
            AudioManager.instance.PlaySFX(3); // play sfx element from audio manager SFX list
            Destroy(gameObject);
            collected = true;
        }
    }

}
