using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody theRB;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position); //look at the transform position of the player controller instance

        theRB.velocity = transform.forward * moveSpeed; //move the enemy forward by the moveSpeed
    }
}
