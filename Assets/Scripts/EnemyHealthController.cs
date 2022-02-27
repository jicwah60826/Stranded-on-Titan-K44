using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject enemyExplosionFX; // Explosion effect for enemy killed
    public int currentHealth;
    public EnemyController theEC;
    public Turret theTurret;
    //public Camera mainCameraForEnemySliderFollow;
    public Canvas enemyHealthSliderCanvas;
    public Slider enemyHealthSlider;
    Vector3 enemyHealthSliderScale;

    private void Awake()
    {
        enemyHealthSlider.maxValue = currentHealth;
        enemyHealthSliderCanvas.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {

        Vector3 v = PlayerController.instance.transform.position - transform.position;
        v.x = v.z = 0.0f;
        enemyHealthSliderCanvas.transform.LookAt(PlayerController.instance.transform.position - v);
        enemyHealthSliderCanvas.transform.Rotate(0, 180, 0);

        //update slider
        UpdateEnemyHealthBar();
    }

    public void DamageEnemy(int damageAmount)
    {
        enemyHealthSliderCanvas.gameObject.SetActive(true);

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

    public void UpdateEnemyHealthBar()
    {
        enemyHealthSlider.value = currentHealth;
    }
}
