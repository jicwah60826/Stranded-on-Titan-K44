using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    public int damage;
    public bool damageEnemy, damagePlayer;
    public float destroyWaitPeriod;
    private float destroyTimer;

    private void Start()
    {
        destroyTimer = destroyWaitPeriod;
    }

    private void Update() {
        DestroyTimer();
    }


    private void OnTriggerEnter(Collider other)
    {
        //check what object tag the bullet collided with
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            Debug.Log("Enemy hit by rocket");
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }


        if (other.gameObject.tag == "Player" && damagePlayer)
        {

            //Debug.Log("Player has been hit at " + transform.position);
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
        }

    }

    private void DestroyTimer()
    {
        //Begin countdown to destroy
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
            //Debug.Log("rocket destroyed from scene");
        }
    }
}
