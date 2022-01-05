using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Define Global Variables
    public float moveSpeed;
    public CharacterController charCon;

    private Vector3 moveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get Keyboard / Game Controller input
        moveInput.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        moveInput.z = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        // Move the player based on the moveInput Vector3
        charCon.Move(moveInput);
    }
}
