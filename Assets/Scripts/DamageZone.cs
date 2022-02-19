using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    //public int damageAmount;
    public bool damageEnemy, damagePlayer;
    private float damageIntervalCounter;

    [Header("Damage Amount")]
    [Tooltip("Minimum random damage intensity")]
    public int minDamageAmount;
    [Tooltip("Maximum random damage intensity")]
    public int maxDamageAmount;
    [Header("Damage Interval")]
    [Tooltip("Minimum random damage interval")]
    public float minDamageInterval;
    [Tooltip("Maximum random damage interval")]
    public float maxDamageInterval;
    private float damageInterval;

    private void Start()
    {
        damageIntervalCounter = damageInterval;
    }

    private void OnTriggerStay(Collider other)
    {
        // assign random damage interval
        float damageInterval = Random.Range(minDamageInterval, maxDamageInterval);



        if (other.gameObject.tag == "Player" && damagePlayer)
        {

            //begin interval timer
            damageIntervalCounter -= Time.deltaTime;
            //Debug.Log("damageIntervalCounter: " + damageIntervalCounter);

            // Begin damage when timer runs out
            if (damageIntervalCounter <= 0)
            {
                // assign random damage amount
                int damageAmount = Random.Range(minDamageAmount, maxDamageAmount);
                Debug.Log("damageAmount = " + damageAmount);
                // reset damage timer
                damageIntervalCounter = damageInterval;
                other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damageAmount);
            }
        }
    }
}
