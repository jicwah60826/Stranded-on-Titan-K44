using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float moveSpeed, lifeTime;
    public Rigidbody theRB;

    public GameObject impactEffect;

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
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
