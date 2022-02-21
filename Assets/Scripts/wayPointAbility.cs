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
                // Update Player Prefs
                hasWayPointPerk = "true";
                PlayerPrefs.SetString("hasWayPointPerk", hasWayPointPerk);
                Debug.Log("hasWayPointPerk has been set to " + hasWayPointPerk);
                // Enable Waypoint system in game
                GameManager.instance.wayPointsEnabled = true;

                // Play sound and destroy game object
                AudioManager.instance.PlaySFX(0); // play sfx element from audio manager SFX list
                Destroy(gameObject);
        }
    }
}
