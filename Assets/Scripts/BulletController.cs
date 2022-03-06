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

    private float disableColliderCounter = .01f;

    private string hitType;

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
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            hitType = "Enemy";
        }

        // Check if headshot to enemy
        if (other.gameObject.tag == "Headshot" && damageEnemy)
        {
            // Call the DamageEnemy script on the parent of this game object
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * headShotMultiplier);
        }

        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            other.gameObject.GetComponent<PlayerHealthController>().DamagePlayer(damage);
            hitType = "Player";
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hitType = "Ground";
        }

        // do bullet impact FX, impact audio and destroy bullet
        impactFX();
    }

    public void impactFX()
    {
        // Do impact Effect
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);

        // Impact sound based on layer type hit
        if (hitType == "Player")
        {
            AudioManager.instance.PlayerShotSFX();
            //AudioManager.instance.PlaySFX(7); // play sfx element from audio manager SFX list
        }

        if (hitType == "Enemy")
        {
            AudioManager.instance.PlaySFX(2); // play sfx element from audio manager SFX list
        }

        if (hitType == "Ground")
        {
            AudioManager.instance.PlaySFX(13); // play sfx element from audio manager SFX list
        }

        disableColliderCounter -= Time.deltaTime; // begin disableColliderCounter countdown

        if (disableColliderCounter <= 0)
        {
            // Disable the collider on the bullet
            bulletCollider.enabled = false;
        }

        // Destroy after 5 seconds
        Destroy(gameObject, 5);


    }
}
