using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem value1;
    [SerializeField] private ParticleSystem value2;
    [SerializeField] private ParticleSystem value3;
    [SerializeField] private Transform parent1;
    [SerializeField] private Transform parent2;
    [SerializeField] private Transform parent3;
    private ParticleSystem.EmissionModule EmissionModule1;
    private ParticleSystem.EmissionModule EmissionModule2;
    private ParticleSystem.EmissionModule EmissionModule3;
   
    
    private Vector3 velocity= Vector3.zero;
    private Camera cam;
    private bool isMoving;

    public Vector3 Velocity => velocity;

    private void Start()
    {
        EmissionModule1 = value1.emission;
        EmissionModule2 = value2.emission;
        EmissionModule3 = value3.emission;
         cam =Camera.main;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            velocity += transform.up * (acceleration * Time.fixedDeltaTime);
        }
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity) * 0.98f;
      
    }

    private void Update()
    {
        EmissionModule1.enabled = false;
        EmissionModule2.enabled = false;
        EmissionModule3.enabled = false;
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
         
            EmissionModule1.enabled = true;
            EmissionModule2.enabled = true;
            EmissionModule3.enabled = true;
            parent1.localEulerAngles = new Vector3(0, 0, -90);
            parent2.localEulerAngles = new Vector3(0, 0, -90);
            parent3.localEulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            isMoving = false;
        }
        
        
        if (Input.GetKey(KeyCode.D))
        {
         transform.Rotate(Vector3.forward * (-rotationSpeed * Time.deltaTime));   
         parent1.localEulerAngles = new Vector3(0, 0, -60);
         parent2.localEulerAngles = new Vector3(0, 0, -60);
         parent3.localEulerAngles = new Vector3(0, 0, -60);
         EmissionModule1.enabled = true;
         EmissionModule2.enabled = true;
         EmissionModule3.enabled = true;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
            parent1.localEulerAngles = new Vector3(0, 0, -120);
            parent2.localEulerAngles = new Vector3(0, 0, -120);
            parent3.localEulerAngles = new Vector3(0, 0, -120);
            EmissionModule1.enabled = true;
            EmissionModule2.enabled = true;
            EmissionModule3.enabled = true;
        }

        transform.position += velocity;
     
        
        Limits();
    }

    private void Limits()
    {
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);

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

        transform.position = cam.ViewportToWorldPoint(screenPos);
    }
}
