using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Transform rotateObj;
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private Vector2 minMaxRotationSpeed;
    private float currentSpeed;
    private float currentSpeedRotation;
    private bool isRotatable;
    private void Start()
    {
        currentSpeed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        isRotatable = Random.value > 0.3f;
        if (isRotatable)
        {
            currentSpeedRotation= Random.Range(minMaxRotationSpeed.x, minMaxRotationSpeed.y);
        }
    }

    private void Update()
    {
        MoveForward(transform, currentSpeed);    
        rotateObj.Rotate(Vector3.forward * (currentSpeedRotation * Time.deltaTime));
       Limits();
    }
    
    private void Limits()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPos.y < 0)
        {
            screenPos.y = 1;
        }
        else if (screenPos.y > 1)
        {
            screenPos.y = 0;
        }
        
        if (screenPos.x < 0)
        {
            screenPos.x = 1;
        }
        else if (screenPos.x > 1)
        {
            screenPos.x = 0;
        }

        transform.position = Camera.main.ViewportToWorldPoint(screenPos);
    }
    public void MoveForward(Transform asteroid, float movementSpeed)
    {
        asteroid.Translate(movementSpeed * Time.deltaTime * Vector2.up);
    }
}
