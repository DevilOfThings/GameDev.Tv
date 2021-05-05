using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;

    [SerializeField]
    Vector3 movementVector;

    [SerializeField]
    [Range(0,1)]
    float movementFactor;
    [SerializeField]
    float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = GetComponent<Transform>().position;      
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / Math.Max(Mathf.Epsilon,period);
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles*tau);

        movementFactor = (rawSinWave + 1f)/2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
