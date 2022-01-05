using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Define Global Variables
    public float moveSpeed, gravityModifier, mouseSensitivity;
    public bool invertX, invertY;
    public CharacterController charCon;

    private Vector3 moveInput;
    public Transform camTrans;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ///////////*********** Get Keyboard / Game Controller input ********** ///////////

        //moveInput.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        //Store current Y velocity before every update
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horiMove = transform.forward * Input.GetAxisRaw("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        moveInput = moveInput * moveSpeed;

        //Re-Calc Y velocity after above updates are applied
        moveInput.y = yStore;

        //Modify the Y value of the player to be world physics * the gravity modifier. This will control how high or easy we can jump
        moveInput.y  += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if(charCon.isGrounded){
            //if we are on the ground
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        // Move the player based on the moveInput Vector3
        charCon.Move(moveInput * Time.deltaTime);

        ///////////*********** Get Mouse Movement / Control Camera Rotation ********** ///////////
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //Check Invert X or Y axis flips are
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }


        // Apply Y axis rotation (side to side) to player based on Mouse Input
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        // Apply X axis rotation (up and down) to camera rotation based on mouse input. The -mouseinput is so that when we move our mouse up, the camera rotation on the X axis is applied correctly and not inverted
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
}
