using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GunController activeGun;

    public static PlayerController instance;

    // Define Global Variables
    public float moveSpeed, runSpeed, jumpPower, gravityModifier, mouseSensitivity;
    public bool invertX, invertY;
    public CharacterController charCon;

    private Vector3 moveInput;
    public Transform camTrans;
    public Transform groundCheckPoint; // item that will define what is ground
    public LayerMask whatIsGround;
    private bool canJump, canDoubleJump, isRunning;

    public Animator anim;

    public GameObject bullet;

    public Transform firePoint;

    private void Awake()
    {
        instance = this; // allow this script to be accessed anywhere
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmoText();
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
        Vector3 horiMove = transform.right * Input.GetAxisRaw("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();

        // Run or Walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
            isRunning = true;
        }

        else
        {
            moveInput = moveInput * moveSpeed;
            isRunning = false;
        }

        //Re-Calc Y velocity after above updates are applied
        moveInput.y = yStore;

        //Modify the Y value of the player to be world physics * the gravity modifier. This will control how high or easy we can jump
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            //if we are on the ground
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        // check if we are within range of any ground layer object
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        ///////////*********** Handle Jumping ********** ///////////

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                moveInput.y = jumpPower;
                canDoubleJump = true;
            }
            else if (canDoubleJump == true)
            {
                moveInput.y = jumpPower;
                canDoubleJump = false;
            }
        }

        ///////////*********** Move the Player ********** ///////////
        //moveInput = Vector3.Lerp(moveInput, movement, Time.deltaTime * movementAcceleration); // TEST ME
        charCon.Move(moveInput * Time.deltaTime);

        ///////////*********** Rotate Player / Control Camera Rotation ********** ///////////
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

        // Handle Shooting - single shots (each shot delayed by fire rate amount)
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {

            RaycastHit hit; //project  raycast from object

            //check if raycast is hitting anything starting from the camera location. Store values in the"hit" raycase object and go out forward for 50 units.
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                // if raycast hits something greater than 2 units out
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                // if no ray is hit within 50 units, send ray straight out from the camera position and rotation for 30 units
                // just so it looks like it's still shooting at the center of the screen
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            //Instantiate(bullet, firePoint.position, firePoint.rotation);
            FireShot();
        }

        // Auto-firing (if enabled on Gun)
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }

        }

        // Animations
        anim.SetFloat("moveSpeed", moveInput.magnitude); // set the moveSpeed paramater in the animation controller to be the magnitude of how much player is moving
        anim.SetBool("isRunning", isRunning); //set the isRunning param in the Player animation
        anim.SetBool("onGround", canJump); //set the onGround param in the Player animation
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation); // Fire a Shot
            activeGun.fireCounter = activeGun.fireRate; // reset our fireCounter after each shot
            activeGun.currentAmmo--;
            UpdateAmmoText();
        }
    }

    private void UpdateAmmoText()
    {
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo; //ensure active gun current ammo always displayed at start
    }
}
