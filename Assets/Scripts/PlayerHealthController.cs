using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int maximumHealth;
    public int currentHealth;
    public float invicibleLength;
    private float invicibleCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
    }

    private void Update()
    {
        Debug.Log("invicibleCounter: " + invicibleCounter);
        Debug.Log("player health: " + currentHealth);

        if (invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime; // begin invincible countdown once player is first shot
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invicibleCounter <= 0)
        {
            currentHealth -= damageAmount; // de-iterate player health
            if (currentHealth <= 0)
            {
                // Player dead
                Debug.Log("Player has been killed");
                gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        invicibleCounter = invicibleLength;
    }
}
