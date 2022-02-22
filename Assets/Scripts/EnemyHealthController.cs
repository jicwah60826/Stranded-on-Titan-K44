using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject enemyExplosionFX; // Explosion effect for enemy killed

    public int currentHealth = 5;

    public EnemyController theEC;

    public Turret theTurret;

    public void DamageEnemy(int damageAmount)
    {

        currentHealth -= damageAmount; // de-iterate enemy health

        if (theEC != null)
        {
            theEC.EnemyShot();
        }

        if (theTurret != null)
        {
            theTurret.TurretShot();
        }

        AudioManager.instance.PlaySFX(2); // play sfx element from audio manager SFX list
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(enemyExplosionFX, transform.position, transform.rotation);
        }
    }
}
