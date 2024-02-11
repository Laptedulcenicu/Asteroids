using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float nextSpawnTime = 0.0f;

    private const float ONE_HUNDRED_PERCENT = 100.0f, ZERO_PERCENT = 0.0f;
    private const int FIRST_SPAWN_POINT = 0;
    
    [Header("Asteroid spawn settings")]
    [SerializeField] private GameObject asteroid = null;
    [SerializeField] private Transform asteroidLookatPosition = null;
    [SerializeField] private float minAngle = -10.0f;
    [SerializeField] private float maxAngle = 10.0f;

    [Header("Common spawn settings")]
    [SerializeField, Range(0.0f, 100.0f)] private float asteroidSpawnChance = 25.0f;
    [SerializeField, Min(0.0f)] private float createDelay = 1.5f;
    [SerializeField] private Transform[] spawnPoints = null;



    private void Update()
    {
        CreateEntity(asteroid, asteroidLookatPosition, minAngle, maxAngle,
            spawnPoints, createDelay, asteroidSpawnChance);
    }
    
    public void CreateEntity(GameObject asteroid, Transform asteroidLookAtPosition, float asteroidMinAngle, float asteroidMaxAngle, Transform[] spawnPositions, float crateDelay, float chanceToSpawnAsteroid)
    {
        if (Time.time > nextSpawnTime)
        {
            float randomNumber = Random.Range(ZERO_PERCENT, ONE_HUNDRED_PERCENT);
            int randomPosition = Random.Range(FIRST_SPAWN_POINT, spawnPositions.Length);

            if (randomNumber >= chanceToSpawnAsteroid)
            {
               // CreateEnemy(enemy, spawnPositions[randomPosition], player.transform);
            }
            else
            {
                CreateAsteroid(asteroid, spawnPositions[randomPosition], asteroidLookAtPosition, asteroidMinAngle, asteroidMaxAngle);
            }

            nextSpawnTime = Time.time + crateDelay;
        }
    }
    
    private void CreateEnemy(GameObject enemy, Transform position, Transform player)
    {
        GameObject enemyEntity = Object.Instantiate(enemy, position.position, position.rotation);
       // enemyEntity.GetComponent<AIDestinationSetter>().target = player;
    }
    private void CreateAsteroid(GameObject asteroid, Transform position, Transform lookAtPoint, float asteroidMinAngle, float asteroidMaxAngle)
    {
        float xRotationAngle = 0.0f, yRotationAngle = 0.0f, angleRotationOffset = 90.0f;

        float angle = Mathf.Atan2(
                position.position.y - lookAtPoint.transform.position.y,
                position.position.x - lookAtPoint.transform.position.x)
            * Mathf.Rad2Deg + angleRotationOffset;

        float angleOffset = Random.Range(asteroidMinAngle, asteroidMaxAngle);

        Quaternion asteroidRotation = Quaternion.Euler(new Vector3(xRotationAngle, yRotationAngle, angle + angleOffset));

        Instantiate(asteroid, position.position, asteroidRotation);
    }
}
