using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject enemyExplosionFX; // Explosion effect for enemy killed

    public int currentHealth = 5;


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
        AudioManager.instance.PlaySFX(2); // play sfx element from audio manager SFX list
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(enemyExplosionFX, transform.position, transform.rotation);
        }
    }
}
