using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // The interval in seconds at which physics and other fixed frame rate updates (like MonoBehaviour's FixedUpdate) are performed.
    public float fixedUpdateTime = .005f;

    private Object[] textures;

    public float waitAfterDying;
    private float reSpawnTimer;
    [HideInInspector] public bool levelEnding;
    [Header("Way Point Markers")]
    [Tooltip("The parent game object item for ALL waypoint markersa in the game / level")]
    public GameObject wayPointSystem;
    public bool wayPointsAbility;
    public bool wayPointsEnabled;

    public string stringSeed = "seed string";
    public bool useStringSeed;
    public int seed;
    public bool randomizeSeed;

    private void Awake()
    {
        /* This doubles the number of times per second the fixedUpdate will run. You can keep decreasing the multiplier to increase number of checks. */
        Time.fixedDeltaTime = Time.timeScale * fixedUpdateTime; // Normally multiplied by 0.02 to make 50 checks a second.

        textures = Resources.LoadAll("Art", typeof(Texture2D)); // load all textures on game start

        instance = this; // allow this script to be accessed anywhere

        // Random Seed Controller
        if (useStringSeed)
        {
            seed = stringSeed.GetHashCode();
        }

        if (randomizeSeed)
        {
            seed = Random.Range(0, 999999);
        }

        Random.InitState(seed);


    }
    // Start is called before the first frame update
    void Start()
    {
        // Hide / Lock cursor movement in game window
        Cursor.lockState = CursorLockMode.Locked;

        //Set Cursor to not be visible
        Cursor.visible = false;

        PlayerAbilities();
        //PauseUnPause();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }

        PlayerAbilities();
        HandleWayPointMarkers();
        DevCommands();
    }


    private void PlayerAbilities()
    {
        ///// ***** WAY POINT PERK ***** /////

        // get value of wayPointsAbility
        if (PlayerPrefs.HasKey("wayPointsAbility"))
        {
            //Debug.Log("wayPointsAbility exists");

            //set hasWayPoint bool based on wayPointsAbility player pref
            if (PlayerPrefs.GetString("wayPointsAbility") == "true")
            {
                wayPointsAbility = true;
                //Debug.Log("wayPointsAbility = " + wayPointsAbility);
            }
        }
    }

    private void HandleWayPointMarkers()
    {
        if (wayPointSystem != null)
        {
            if (Input.GetKeyDown(KeyCode.Y) && wayPointSystem != null)
            {
                if (wayPointsAbility)
                {
                    // toggle wayPointsEnabled
                    wayPointsEnabled = !wayPointsEnabled;
                    //Debug.Log("wayPointsEnabled set to " + wayPointsEnabled);
                }
                else
                {
                    //Debug.Log("wayPoint perk not earned yet");
                }
            }

            //handle waypoint visibility
            if (wayPointsAbility)
            {
                if (wayPointsEnabled)
                {
                    wayPointSystem.SetActive(wayPointsEnabled);
                }

            }
            else
            {
                // hide waypoints if player does not haver the perk
                wayPointSystem.SetActive(false);
            }

            if (wayPointsEnabled)
            {
                wayPointSystem.SetActive(true);
            }
            else
            {
                wayPointSystem.SetActive(false);
            }
        }
        else
        {
            //Debug.Log("waypoint system needs to be assigned in game manager");
        }
    }

    private void ClearPlayerPrefs()
    {
        //PlayerPrefs.DeleteKey("wayPointsAbility");
        PlayerPrefs.SetString("wayPointsAbility", "false");
        PlayerPrefs.SetInt("maximumHealth", 100);
        PlayerPrefs.SetInt("MetalCount", 0);
        //PlayerPrefs.DeleteKey("MetalCount");
        wayPointsAbility = false;
        wayPointsEnabled = false;
        PlayerController.instance.useGunsAbility = false;
        PlayerController.instance.runAbility = false;
        PlayerController.instance.jumpAbility = false;
        PlayerController.instance.doubleJumpAbility = false;
        PlayerController.instance.flashLightAbility = false;
        // Update UI elements
        PlayerHealthController.instance.UpdateHealthBarText();
    }

    private void DevCommands()
    {

#if UNITY_EDITOR

        /////***** CLEAR WAYPOINT ABILITY *****/////

        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearPlayerPrefs();
            Debug.Log("PlayerPrefs removed by player");
        }

        /////***** TOGGLE PLAYER RECEIVE DAMAGE *****/////

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Toggle the player receiving damage or not
            PlayerHealthController.instance.receiveDamage = !PlayerHealthController.instance.receiveDamage;
            if (PlayerHealthController.instance.receiveDamage == false)
            {
                //Debug.Log("player is now invincible!");
                //reset player health
                PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maximumHealth;
                PlayerHealthController.instance.UpdateHealthBarText();
            }
            else
            {
                //Debug.Log("player can receive damage");
            }
        }

        /////***** TOGGLE PLAYER INFINITE AMMO *****/////

        if (Input.GetKeyDown(KeyCode.X))
        {
            // Toggle usage of ammo
            PlayerController.instance.useAmmo = !PlayerController.instance.useAmmo;
        }

#endif

    }


    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // get the name of the currently active scene and re-load / start it over.

        ClearPlayerPrefs();
    }

    public void PauseUnPause()
    {

        // NOTE: this function is only called when we hit the escape key

        // If pause menus is active in the hierarchy - then disable it and return to the game
        if (UIController.instance.pauseScreenOverlay.activeInHierarchy)
        {
            //Debug.Log("zz1: GameManager: pauseScreenOverlay is ACTIVE");

            // un-pause all sounds in game
            //AudioListener.pause = false;

            // disable the pause menu overlay
            UIController.instance.pauseScreenOverlay.SetActive(false);
            //Debug.Log("zz2: GameManager: pauseScreenOverlay has been DISABLED");

            //re-enable the Pause Menu Main for future use
            UIController.instance.pauseMenuMain.SetActive(true);

            //Disable all sub-menus
            UIController.instance.audioSubMenu.SetActive(false);
            UIController.instance.controlsSubMenu.SetActive(false);
            UIController.instance.confirmQuit.SetActive(false);


            // lock the cursor back onto the screen
            Cursor.lockState = CursorLockMode.Locked;

            // un-freeze time
            Time.timeScale = 1f;

            // Hide cursor
            Cursor.visible = false;
        }
        else
        {
            // enable the pause menu overlay
            UIController.instance.pauseScreenOverlay.SetActive(true);
            //Debug.Log("zz3: GameManager: pauseScreenOverlay is now ACTIVE");

            // pause all sounds in game
            //AudioListener.pause = true;

            // unlock the cursor so user can navigate the menus
            Cursor.lockState = CursorLockMode.None;

            // Show Cursor
            Cursor.visible = true;

            // freeze time
            Time.timeScale = 0f;
        }
    }
}
