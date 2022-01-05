using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField] Vector3 MovementVector;
    float MovementFactor;
    [SerializeField] float period = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        /***** BEGIN: Build Sin wave and use to oscillate the movement *****/
        if (period <= Mathf.Epsilon) { return; } //stops all of the below if period resolves to 0 for some reason. To avoid a NaN. Mathf.Epsilon is the smallest value of a float number.
        float cycles = Time.time / period; //continually grows over time
        const float tau = Mathf.PI * 2; // constanmt value of ~ 6.283 (takes value of PI and multiples by 2. tau is the mathematical for a full circle.)
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        MovementFactor = (rawSinWave + 1f) / 2; // recalculated to go from 0 to 2 so it's cleaner
        /***** END: Build Sin wave and use to oscillate the movement *****/
        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingPosition + Offset;
    }
}