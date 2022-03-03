using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GunController activeGun;

    public static PlayerController instance;

    // Define Global Variables
    public float moveSpeed, runSpeed, jumpPower, gravityModifier, mouseSensitivity, adsSpeed, muzzFlashDelay;
    public bool useAmmo, runAbility, jumpAbility, doubleJumpAbility, flashLightAbility, useGunsAbility, wayPointsAbility;
    [HideInInspector]
    public bool canRun = true;
    [HideInInspector]
    public bool gunHolstered;
    [HideInInspector]
    public CharacterController charCon;
    public Transform camTrans, firePoint, groundCheckPoint /* item that will define WHERE the ground is */, adsPoint, gunHolder;
    public LayerMask whatIsGround; // Layer mask that defines WHAT the ground is
    public GameObject flashLight, muzzeFlash, playerGuns;
    public Animator anim;
    public List<GunController> allGuns = new List<GunController>();
    public int currentGun;
    public List<GunController> unlockableGuns = new List<GunController>();

    //booster boots
    public bool boosterBootsAbility;
    [HideInInspector]
    public bool canBoostBoots;
    [Tooltip("The amount of time space bar must be held before boost boots are allowed")]
    public float boostBootsWaitTime, boosterBootPower;
    private float boosBootTimer;

    private Vector3 moveInput, gunStartPos;
    private float bounceAmount;
    private bool bounce, canJump, canDoubleJump, isRunning, lightOn, hasStamina = true, onGround, isFalling;

    public float maximumStamina, maxCamTiltAngle;
    private float currentStamina, fallVelocity;
    public float staminaDrainSpeed; // used for giving buffs or debuffs to the stamina DRAIN speed
    public float staminaRegainSpeed; // used for giving buffs or debuffs to the stamina REGAIN speed
    public float waitToRegainStamina; // amount of wait time before stamina begins to go back up
    private bool isStaminaCoRoutineExecuting = false;
    private bool previouslyGrounded, isOnGround;
    public float fallThresholdVelocity;

    private void Awake()
    {
        instance = this; // allow this script to be accessed anywhere
    }

    // Start is called before the first frame update
    void Start()
    {
        gunHolstered = true;

        // initialize boost boots timer timer
        boosBootTimer = boostBootsWaitTime;

        currentStamina = maximumStamina;
        UIController.instance.staminaSlider.maxValue = maximumStamina; //set stamina slider max value
        currentGun--; //de-iterate the active gun index
        ToggleHolster();
        SwitchGun(); //invoke switch gun function

        gunStartPos = gunHolder.localPosition;

    }
    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pauseScreenOverlay.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            ToggleFlashlight();
            ToggleHolster();
            PlayerMovement();
            HandleShooting();
            HandleStamina();
            GunSwitching();
            AimDownSight();
            PlayerAnimations();
            HandleBoosterBoots();
        }

    }

    ////////////*****************     FUNCTIONS *****************////////////

    private void PlayerMovement()
    {
        ///////////*********** Get Keyboard / Game Controller input ********** ///////////

        //Store current Y velocity before every update
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxisRaw("Horizontal");


        moveInput = horiMove + vertMove;
        moveInput.Normalize();

        // Run or Walk
        if (Input.GetKey(KeyCode.LeftShift) && canRun && !isStaminaCoRoutineExecuting && runAbility)
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

        // store if player was just on the ground (we do this BEFORE our next check to see if they are CURRENTLY on the ground)
        bool previousGrounded = isOnGround;

        // check if player is on the grouind (if we are within range of any ground layer object)
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;
        isOnGround = canJump;

        HandleFallDamage(previousGrounded);

        HandleJumping();

        ///////////*********** Move the Player ********** ///////////
        //moveInput = Vector3.Lerp(moveInput, movement, Time.deltaTime * movementAcceleration); // TEST ME
        charCon.Move(moveInput * Time.deltaTime);

        ///////////*********** Rotate Player / Control Camera Rotation ********** ///////////
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;


        // Apply Y axis rotation (side to side) to player based on Mouse Input
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        // Apply X axis rotation (up and down) to camera rotation based on mouse input. The -mouseinput is so that when we move our mouse up, the camera rotation on the X axis is applied correctly and not inverted
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));


        /* Build in Camera Tilt protection from little kids like Grahame and Leo moving mouse too fast and flipping camera upside down. */
        if (camTrans.rotation.eulerAngles.x > maxCamTiltAngle && camTrans.rotation.eulerAngles.x < 180f)
        {
            camTrans.rotation = Quaternion.Euler(maxCamTiltAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
        }
        else if (camTrans.rotation.eulerAngles.x > 180f && camTrans.rotation.eulerAngles.x < 360f - maxCamTiltAngle)
        {
            camTrans.rotation = Quaternion.Euler(-maxCamTiltAngle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);

        }

    }

    private void HandleFallDamage(bool previousGrounded)
    {
        if (!previousGrounded && isOnGround && charCon.velocity.y < -fallThresholdVelocity)
        {
            fallVelocity = Mathf.Abs(charCon.velocity.y);

            if (charCon.velocity.y < -fallThresholdVelocity)
            {
                //store the amount we fell so that we can damage player accordingly
                float fallDamageAmount = Mathf.Abs(fallVelocity + fallThresholdVelocity); // ensure we return a positive value
                fallDamageAmount = fallDamageAmount / 3f;
                Debug.Log("Damage Dealt = " + fallDamageAmount);
                int damageAmount = (int)fallDamageAmount; // convert from float to int so we can pass into DamagePlayer function
                PlayerHealthController.instance.DamagePlayer(damageAmount);
                AudioManager.instance.PlaySFX(7); // play sfx element from audio manager SFX list
            }
        }
    }

    private void HandleStamina()
    {
        // begin decreasing the stamina bar if player is running
        if (isRunning == true)
        {
            if (currentStamina > 0)
            {
                currentStamina -= Time.deltaTime * staminaDrainSpeed;
            }
            else
            {
                currentStamina = 0;
            }

        }
        else
        // // begin increasing the stamina bar after coRoutine is done running
        {
            if (!isStaminaCoRoutineExecuting)
            {
                currentStamina += Time.deltaTime * staminaRegainSpeed; // increase stamina bar back over time
            }
        }

        // ensure we don't exceed max stamina allowed
        if (currentStamina >= maximumStamina)
        {
            currentStamina = maximumStamina;
        }

        // Define if player has stamina or not
        if (currentStamina > 0)
        {
            hasStamina = true;
            canRun = true;
        }
        else
        {
            hasStamina = false;
            canRun = false;
            //start coroutine
            StartCoroutine(StaminaDepletedCo());
        }

        //Debug.Log("hasStamina = " + hasStamina);

        // update stamina bar per the currentStamina amount
        UIController.instance.staminaSlider.value = currentStamina;
        //Debug.Log("currentStamina: " + currentStamina);
    }

    private IEnumerator StaminaDepletedCo()
    {

        if (isStaminaCoRoutineExecuting == true)
            yield break; //leave the function is it already executing...

        isStaminaCoRoutineExecuting = true;
        //Debug.Log("isStaminaCoRoutineExecuting: " + isStaminaCoRoutineExecuting);
        yield return new WaitForSeconds(waitToRegainStamina);
        hasStamina = true;
        canRun = true;
        isStaminaCoRoutineExecuting = false;
        //Debug.Log("isStaminaCoRoutineExecuting: " + isStaminaCoRoutineExecuting);
    }

    private void HandleJumping()
    {
        ///////////*********** Handle Jumping ********** ///////////

        // only allow jumping if player has stamina
        if (hasStamina == true && !isStaminaCoRoutineExecuting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump && jumpAbility)
                {
                    moveInput.y = jumpPower;
                    canDoubleJump = true;
                    AudioManager.instance.PlaySFX(8); // play sfx element from audio manager SFX list
                }
                else if (canDoubleJump && doubleJumpAbility)
                {
                    moveInput.y = jumpPower;
                    AudioManager.instance.PlaySFX(8); // play sfx element from audio manager SFX list
                    canDoubleJump = false;
                }
            }

            // Handle Bouncing
            if (bounce)
            {
                bounce = false; //toggle the bounce bool
                moveInput.y = bounceAmount; // applty the bounce
                canDoubleJump = true; // allow double jump if bouncepad hit
            }
        }
    }

    private void HandleBoosterBoots()
    {
        if (!boosterBootsAbility)
        {
            return;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            boosBootTimer -= Time.deltaTime;

            if (boosBootTimer <= 0)
            {
                canBoostBoots = true;
                if (boosterBootsAbility)
                {
                    // Gravity test
                    gravityModifier = boosterBootPower;

                }
            }
        }
        else
        {
            gravityModifier = 2f;
            boosBootTimer = boostBootsWaitTime;

        }
    }

    private void PlayerAnimations()
    {
        // Animations
        anim.SetFloat("moveSpeed", moveInput.magnitude); // set the moveSpeed paramater in the animation controller to be the magnitude of how much player is moving
        anim.SetBool("isRunning", isRunning); //set the isRunning param in the Player animation
        anim.SetBool("onGround", canJump); //set the onGround param in the Player animation
    }

    private void GunSwitching()
    {
        if (!useGunsAbility) { return; }

        // Run Switch Gun when tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
            AudioManager.instance.PlaySFX(10); // play sfx element from audio manager SFX list
        }
    }

    private void HandleShooting()
    {

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

        // Multiple Shots / Auto-Firing (if enabled on gun)
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
                //("Call FireShot function");
            }

        }
    }

    public IEnumerator MuzzleFlash()
    {
        muzzeFlash.SetActive(true);
        yield return new WaitForSeconds(muzzFlashDelay);
        muzzeFlash.SetActive(false);
    }

    private void AimDownSight()
    {
        //Handle Aim down sight
        if (Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(activeGun.zoomAmount);

        }

        if (Input.GetMouseButton(1))
        {
            //while mouse button is being held down
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);

        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, adsSpeed * Time.deltaTime);

        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();

        }
    }

    public void FireShot()
    {
        if (useGunsAbility && !gunHolstered)
        {
            if (activeGun.currentAmmo > 0)
            {
                Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation); // Fire a Shot
                StartCoroutine(MuzzleFlash()); // muzzle flash coroutine
                activeGun.fireCounter = activeGun.fireRate; // reset our fireCounter after each shot
                if (useAmmo)
                {
                    activeGun.currentAmmo--;
                }
                UpdateAmmoText();
            }
        }
    }

    private void UpdateAmmoText()
    {
        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo; //ensure active gun current ammo always displayed at start
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false); //de-activate current game object (note: they are de-activated in the scene by default)

        currentGun++; // iterate the current gun. If we had a -1 for example (from when the de-teration occurred in the start function), the active gun is now 0.

        // We then continue on with the below. The updateAmmo text is run at game start now as well as when we do an actual switch gun via tab key.

        if (currentGun >= allGuns.Count)
        {
            currentGun = 0; //reset back to start if on last gun. Allows to loop through the guns
        }

        //select a new, then set it to active
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        UpdateAmmoText();

        firePoint.position = activeGun.firePoint.position; /* set the firePoint game object on the player controller to be the position of the firePoint gameobject of the gun that is now active */
    }

    public void ToggleFlashlight()
    {
        if (!flashLightAbility)
        {
            return;
        }

        if (Input.GetMouseButtonDown(2))
        {
            lightOn = !lightOn;
            flashLight.SetActive(lightOn);
            AudioManager.instance.PlaySFX(11); // play sfx element from audio manager SFX list
        }
    }

    public void ToggleHolster()
    {

        if (Input.GetKeyDown(KeyCode.H) && useGunsAbility)
        {
            gunHolstered = !gunHolstered;
            AudioManager.instance.PlaySFX(10); // play sfx element from audio manager SFX list
        }
        if (gunHolstered)
        {
            UIController.instance.crosshairs.SetActive(false);
            UIController.instance.ammoCounter.SetActive(false);
            UIController.instance.centerDot.SetActive(true);
            playerGuns.SetActive(false);
        }
        if (!gunHolstered)
        {
            UIController.instance.crosshairs.SetActive(true);
            UIController.instance.ammoCounter.SetActive(true);
            UIController.instance.centerDot.SetActive(false);
            playerGuns.SetActive(true);
        }
    }

    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count > 0)
        {
            for (int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;

                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.Remove(unlockableGuns[i]);

                    i = unlockableGuns.Count;
                }
            }
        }

        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2; // set the current gun to the highest level gun to ensure when we switch guns, it is switched to the gun we just picked up.
            SwitchGun();
        }
    }

    public void Bounce(float bounceForce)
    {

        bounceAmount = bounceForce;
        bounce = true;


    }
}
