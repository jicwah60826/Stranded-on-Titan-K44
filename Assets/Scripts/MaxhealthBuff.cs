using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxhealthBuff : MonoBehaviour
{
    public int maxHealthIncreaseAmount;
    private int playerPrefMaxHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list
            Destroy(gameObject);
        }
    }
}
