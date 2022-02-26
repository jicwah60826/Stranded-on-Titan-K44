using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPickup : MonoBehaviour
{
    public float airAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.GiveAir(airAmount);
            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);
        }
    }
}
