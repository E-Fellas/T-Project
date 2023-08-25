using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_UpAndDown : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float amplitude = 1.0f; //Altura
    public float frequency = 1.0f; //O quão rapido oscila

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 newPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * frequency)* amplitude;

        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
}
