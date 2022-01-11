using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    public int damage;
    public int headShotMultiplier;

    public bool damageEnemy, damagePlayer;

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed; //moves the rigidbody along the Z axis

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check what object tag the bullet collided with

        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            // Destroy(other.gameObject);
            Debug.Log("Enemy Bodyshot");
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        // Check if headshot to enemy
        if (other.gameObject.tag == "Headshot" && damageEnemy)
        {
            Debug.Log("Enemy Headshot");
            // Call the DamageEnemy script on the parent of this game object
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * headShotMultiplier);
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            // Destroy(other.gameObject);
            Debug.Log("Player has been hit at " + transform.position);
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}
