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

            Destroy(gameObject);
            collected = true;
        }
    }

}
