using System;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{

    public float frequency;
    public float magnitude;
    public Vector3 direction;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + direction * (magnitude * MathF.Sin(Time.time * frequency));
    }
}
