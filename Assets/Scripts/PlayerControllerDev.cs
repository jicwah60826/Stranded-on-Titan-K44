using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDev : MonoBehaviour
{

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
        moveInput.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        moveInput.z = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        charCon.Move(moveInput);
    }
}
