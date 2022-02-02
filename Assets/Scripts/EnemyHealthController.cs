using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject enemyExplosionFX; // Explosion effect for enemy killed

    public int currentHealth = 5;

    public EnemyController theEC;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageEnemy(int damageAmount)
    {

        currentHealth -= damageAmount; // de-iterate enemy health

        // check if this enemy has an EnemyController script attached
        if (theEC != null)
        {
            // call the GetShot function within the EnemyController script
            theEC.GetShot();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(enemyExplosionFX, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(2); // play sfx element from audio manager SFX list
        }
    }
}
