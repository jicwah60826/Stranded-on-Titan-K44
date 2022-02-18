using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    public Collider bulletCollider;

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
            //Debug.Log("Enemy Bodyshot");
            impactFX();
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        /*         if (other.gameObject.tag == "Turret" && damageEnemy)
                {
                    impactFX();
                    other.gameObject.GetComponent<TurretHealthController>().DamageTurret(damage);
                } */

        // Check if headshot to enemy
        if (other.gameObject.tag == "Headshot" && damageEnemy)
        {
            //Debug.Log("Enemy Headshot");
            impactFX();
            // Call the DamageEnemy script on the parent of this game object
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * headShotMultiplier);
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {

            //Debug.Log("Player has been hit at " + transform.position);
            impactFX();
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
        }
        // do bullet impact and destroy bullet
        impactFX();
    }

    public void impactFX()
    {
        // Do impact Effect
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);

        // Disable the collider on the bullet
        bulletCollider.enabled = false;

        // Destroy after 5 seconds
        Destroy(gameObject, 5);


    }
}
