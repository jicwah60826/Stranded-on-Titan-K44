using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRings : MonoBehaviour
{


    public int damage;

    public bool damageEnemy, damagePlayer;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
        }
    }
}
