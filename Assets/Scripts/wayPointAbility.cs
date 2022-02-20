using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointAbility : MonoBehaviour
{
    private string hasWayPointPerk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (!PlayerPrefs.HasKey("hasWayPointPerk"))
            {
                // Update Player Prefs
                hasWayPointPerk = "true";
                PlayerPrefs.SetString("hasWayPointPerk", hasWayPointPerk);
                Debug.Log("hasWayPointPerk has been set to " + hasWayPointPerk);

                // Play sound and destroy game object
                AudioManager.instance.PlaySFX(0); // play sfx element from audio manager SFX list
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Player already has wayPoint perk - no action taken");
            }
        }
    }
}
