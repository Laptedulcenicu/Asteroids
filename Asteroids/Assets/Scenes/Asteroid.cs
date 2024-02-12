using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private Transform rotateObj;
    [SerializeField] private Vector2 minMaxSpeed;
    [SerializeField] private Vector2 minMaxRotationSpeed;
    [SerializeField] private GameObject smallAsteroid = null;
    [SerializeField, Min(0.0f)] private float spawnRadius = 1.0f;
    [SerializeField, Min(0)] private int minAmountOfSmallAsteroids = 1;
    [SerializeField, Min(0)] private int maxAmountOfSmallAsteroids = 3;
    
    public float currentSpeed;
    private float currentSpeedRotation;
    private bool isRotatable;
    private void Start()
    {
        if (currentSpeed == 0)
        {
            currentSpeed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        }
       
        isRotatable = Random.value > 0.3f;
        if (isRotatable)
        {
            currentSpeedRotation= Random.Range(minMaxRotationSpeed.x, minMaxRotationSpeed.y);
        }
    }

    private void Update()
    {
        MoveForward(transform, currentSpeed);
        if (rotateObj)
        {
            rotateObj.Rotate(Vector3.forward * (currentSpeedRotation * Time.deltaTime));
        }
        Limits();
    }

    public void Hit()
    {
        if (smallAsteroid)
        {
            CreateAmountAsteroids(smallAsteroid, transform, minAmountOfSmallAsteroids, maxAmountOfSmallAsteroids, spawnRadius);
        }

        Instantiate(particles, transform.position, quaternion.identity);
        UIController.OnKill?.Invoke();
        Destroy(gameObject);
        
    }
    public void CreateAmountAsteroids(GameObject asteroid, Transform currentPosition, int minAmountOfSmallAsteroids, int maxAmountOfSmallAsteroids, float spawnRadius)
    {
        int amountOfAsteroids = Random.Range(minAmountOfSmallAsteroids, maxAmountOfSmallAsteroids);

        for (int i = 0; i < amountOfAsteroids; i++)
        {
          var newObj=  Instantiate(asteroid, (Vector2)currentPosition.position + (Random.insideUnitCircle * spawnRadius), currentPosition.rotation);
          newObj.GetComponent<Asteroid>().currentSpeed = currentSpeed * 1.5f;
        }
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
