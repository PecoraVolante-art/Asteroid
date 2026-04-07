using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] bigAsteroidsPrefab;
    public GameObject[] mediumAsteroidPrefab;
    public GameObject[] smallAsteroidPrefab;
    public GameObject player;

    public static int playerLives = 3;
    public static int playerscore;

    public Dictionary<AsteroidManager.Type, GameObject[]>asteroids;

    public static GameManager Instance { get; private set; }


    public int numInitialAsteroidCount = 3;
    public float spawnRadius;

    public int numCurrentBigAsteroids;
    public int numCurrentAsteroids;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);


        asteroids = new Dictionary<AsteroidManager.Type, GameObject[]>
        {
            { AsteroidManager.Type.Big, bigAsteroidsPrefab },
            { AsteroidManager.Type.Medium, mediumAsteroidPrefab },
            { AsteroidManager.Type.Small, smallAsteroidPrefab},
        };

         numCurrentBigAsteroids =0;
         numCurrentAsteroids =0;
         SpawnInitialAsteroids();
    }




    void Update()
    {
            RespawnAsteroids();
    }

    private void SpawnInitialAsteroids()
    {
        for (int i = 0; i < numInitialAsteroidCount; i++)
        {
            float randomAngle = Random.Range(0f,360f);

            Vector3 spawnPos = new Vector3(Mathf.Cos(randomAngle) * spawnRadius, Mathf.Sin(randomAngle) * spawnRadius, 0);
            SpawnAsteroid(spawnPos,AsteroidManager.Type.Big);
        }
    }

    private void RespawnAsteroids()
    { 
    if (numCurrentBigAsteroids < numInitialAsteroidCount)
        {
            float randomAngle = Random.Range(0f,360f);
            Vector3 spawnPos = new Vector3(Mathf.Cos(randomAngle) * (spawnRadius + 1 ), Mathf.Sin(randomAngle) * (spawnRadius+1), 0);
            SpawnAsteroid(spawnPos, AsteroidManager.Type.Big);
        }
    }

    public void SpawnAsteroid(Vector3 position, AsteroidManager.Type type)
    {
        GameObject asteroidPrefab = asteroids[type][Random.Range(0, asteroids[type].Length )];
        Instantiate(asteroidPrefab,position,Quaternion.identity);

        if (type == AsteroidManager.Type.Big)
        {
            numCurrentBigAsteroids++;
        }

        numCurrentAsteroids++;


    }

    public void Restart()

    {
        playerLives = 3;
        playerscore = 0;
        DestroyAllAsteroids();

        numCurrentAsteroids = 3;
        numCurrentBigAsteroids = 11;

        SpawnInitialAsteroids();

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.position = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.rotation = 0f;


        Time.timeScale = 1;

    }
    public void DestroyAllAsteroids()
    {
        AsteroidManager[] asteroids = FindObjectsByType<AsteroidManager>(FindObjectsSortMode.None);

        foreach (AsteroidManager asteroid in asteroids)
        {
            Destroy(asteroid.gameObject);
        }
    }
   
}
