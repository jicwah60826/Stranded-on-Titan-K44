using UnityEngine;

public class BoosterBootTester : MonoBehaviour
{

    // Timer controls
    private float startTime = 0f;
    private float timer = 0f;

    // BoosterBootTester Boots Controls
    public bool boosterBootsAbility;
    // [HideInInspector]
    public bool canBoostBoots = false;
    [Tooltip("The amount of time space bar must be held before boost boots are allowed")]
    public float boostBootsWaitTime, boosterBootPower;
    private float boosBootTimer;

    // Use if you only want to call the method once after holding for the required time

    public bool isBoosting; //turn to private when done
    public bool wasBoosting; //turn to private when done

    private void Start()
    {
        //flip the boost power so it's a negative number to impact gravity
        boosterBootPower = -boosterBootPower * .1f; //lessen the impact
        Debug.Log("boosterBootPower set to: " + boosterBootPower);
    }

    void Update()
    {
        if (!boosterBootsAbility)
        {
            return;
        }

        // Starts the timer from when the key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            timer = startTime;
        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetKey(KeyCode.Space) && canBoostBoots == false)
        {
            timer += Time.deltaTime;

            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timer > (startTime + boostBootsWaitTime))
            {
                canBoostBoots = true;
                StartBoost();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopBoost();
        }
    }

    // Method called after held for required time
    public void StartBoost()
    {
        Debug.Log("held for " + boostBootsWaitTime + " seconds");
        PlayerController.instance.gravityModifier = boosterBootPower;
        AudioManager.instance.PlaySFX(12);
    }

    public void StopBoost()
    {
        PlayerController.instance.gravityModifier = 2f;
        canBoostBoots = false;
        startTime = 0f;
        timer = 0f;
        AudioManager.instance.StopSFX(12);
    }
}