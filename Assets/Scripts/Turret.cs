using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject bullet;
    public float rangeToTargetPlayer, timeBetweenShots;
    private float shotCounter;
    public float rotateSpeed;
    public float rotateOffset;
    public float lockOnSpeed; // speed at which the turret turns towards the player once in range

    public Transform gun, firePoint;

    private void Start()
    {
        shotCounter = timeBetweenShots; //initialize shot counter
    }

    private void Update()
    {
        handleShooting();
    }

    private void handleShooting()
    {

        if (PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer)
            {
                Debug.Log("Player now within range of turret");

                /* SMOOTHLY rotate the turret to face the plater once player is within range.
                The Code below takes into account the CURRENT rotation angle of the turret so that
                it rotates from that point to the direction of the player no matter where it is in the rotation cycle
                */

                firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                Vector3 newDirection = Vector3.RotateTowards(gun.forward, PlayerController.instance.transform.position - transform.position, rotateSpeed * lockOnSpeed * Time.deltaTime, 0f);
                gun.rotation = Quaternion.LookRotation(newDirection);

                // begin shot counter countdown to see if turret should fire another shot
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    //Fire a bullet
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots; //reset shot counter
                }
            }
            else
            {
                Debug.Log("Player not within range of turret");
                shotCounter = timeBetweenShots; //reset shot counter
                gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + rotateOffset, 0f), rotateSpeed * Time.deltaTime); // rotate the turret around based on rotateSpeed
            }
        }
    }
}