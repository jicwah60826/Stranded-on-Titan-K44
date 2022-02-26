using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject enemyExplosionFX; // Explosion effect for enemy killed

    public Canvas enemyHealthBar;

    public int currentHealth = 5;

    public EnemyController theEC;

    public Turret theTurret;

    private void LateUpdate()
    {
        if (enemyHealthBar != null)
        {
            enemyHealthBar.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }

    }

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
