using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    [SerializeField] Enemy enemy;
    [SerializeField] float spawnDelay = 1.0f;
    [SerializeField] float spawnInterval = 20;

    float spawnTimer = 0;
    float spawnDelayTimer = 0;
    [SerializeField] int enemiesPerWave = 10;
    int spawnedEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            spawnDelayTimer += Time.deltaTime;
            if (spawnDelayTimer > spawnDelay)
            {
                Enemy e = Instantiate(enemy, transform.position, transform.rotation);
                e.SetEndPoint(endPoint.position);

                spawnedEnemies++;
                spawnDelayTimer = 0;
                if (spawnedEnemies >= enemiesPerWave)
                {
                    spawnTimer = 0;
                    spawnedEnemies = 0;
                }
            }
        }

    }
}
