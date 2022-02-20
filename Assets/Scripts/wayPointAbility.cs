using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointAbility : MonoBehaviour
{
    private string wayPointsAllowed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wayPointsAllowed = "true";
            PlayerPrefs.SetString("wayPointsAllowed", wayPointsAllowed);
            if (PlayerPrefs.GetString("wayPointsAllowed") == "true")
            {
                Debug.Log("waypoints are now allowed");
            }
            AudioManager.instance.PlaySFX(0); // play sfx element from audio manager SFX list
            Destroy(gameObject);
        }
    }
}
