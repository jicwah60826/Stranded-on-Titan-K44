using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RadialIndicatorCllick : MonoBehaviour
{

    [Header("Radial Timers")]
    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null; //set to null so that if left blank unity won't throw error

    [Header("Key Codes")]
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;

    [Header("Unity Event")]
    [SerializeField] private UnityEvent myEvent = null;

    private bool shouldUpdate = false;

    void Update()
    {
        if (Input.GetKey(selectKey))
        {
            shouldUpdate = false;
            indicatorTimer -= Time.deltaTime;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer;

            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;
                radialIndicatorUI.enabled = false;
                myEvent.Invoke();
            }
        }
        else
        {
            if (shouldUpdate)
            {
                indicatorTimer += Time.deltaTime;
                radialIndicatorUI.fillAmount = indicatorTimer;

                if (indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    radialIndicatorUI.fillAmount = maxIndicatorTimer;
                    radialIndicatorUI.enabled = false;
                    shouldUpdate = false;
                }
            }
        }

        if (Input.GetKeyUp(selectKey))
        {
            shouldUpdate = true;
        }

    }
}
