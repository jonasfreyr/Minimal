using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class moveground : MonoBehaviour
{
    public float speed;
    public GameObject ground;
    public GameObject cloud;
    public GameObject tree;
    public GameObject bird;
    public GameObject bone;
    
    public int cloudSpawnRangeStart;
    public int cloudSpawnRangeEnd;
    public float cloudSpawnRate;

    public float _birdSpawnRate;
    public float _birdSpawnRangeEnd;
    public float _birdSpawnRangeStart;
    
    public List<GameObject> obstacles;

    private float _obstacleSpawnRate;

    private void Start()
    {
        StartCoroutine(SpawnClouds());
        StartCoroutine(SpawnBirds());
    }

    public void StartGame()
    {
        _obstacleSpawnRate = 0;
        _obstacleSpawnRate = 1.5f;
        
        InvokeRepeating("SpawnTrees", 5f, 5f);
        InvokeRepeating("SpawnBones", 2f, 2f);

        StartCoroutine(SpawnObstacle());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var groundCounter = 0;

        foreach (Transform child in transform)
        {
            var currSpeed = speed;
                
                 if (child.CompareTag("Ground")) groundCounter++;
            else if (child.CompareTag("Bird"))   currSpeed *= 1.2f;
            else if (child.CompareTag("Tree"))   currSpeed *= .9f;
            else if (child.CompareTag("Cloud"))  currSpeed *= .8f;
            
            switch (GameManager.instance.gameStarted)
            {
                case false when child.CompareTag("Cloud") || child.CompareTag("Bird"):
                    child.position += new Vector3(-currSpeed * 0.2f, 0, 0);
                    break;
                case true:
                    child.position += new Vector3(-currSpeed, 0, 0);
                    break;
            }

        }

        if (groundCounter < 2)
        {
            Instantiate(ground, new Vector3(52, -5, 0), Quaternion.identity, transform);
        }
    }
    
    private IEnumerator SpawnObstacle()
    {
        var currentTime = 0f;
        while (true)
        {

            if (currentTime > _obstacleSpawnRate)
            {
                currentTime = 0f;

                var index = Random.Range(0, obstacles.Count);

                Instantiate(obstacles[index], new Vector3(52, 5), Quaternion.identity, transform);
            }

            currentTime += Time.deltaTime;
            
            yield return 0;
        }
    }
    
    private IEnumerator SpawnBirds()
    {
        var currentTime = 0f;
        while (true)
        {
            var spawnRate = _birdSpawnRate;

            if (!GameManager.instance.gameStarted) spawnRate *= 5;
            
            if (currentTime > spawnRate)
            {
                currentTime = 0f;

                Instantiate(bird, new Vector3(52, Random.Range(_birdSpawnRangeStart, _birdSpawnRangeEnd), -1479.249f), Quaternion.identity, transform);
            }

            currentTime += Time.deltaTime;
            
            yield return 0;
        }
    }
    
    private IEnumerator SpawnClouds()
    {
        var currentTime = 0f;
        while (true)
        {
            
            var spawnRate = cloudSpawnRate;
            
            if (!GameManager.instance.gameStarted) spawnRate *= 5;
            
            if (currentTime > spawnRate)
            {
                currentTime = 0f;

                Instantiate(cloud, new Vector3(52, Random.Range(cloudSpawnRangeStart, cloudSpawnRangeEnd)), Quaternion.identity, transform);
            }

            currentTime += Time.deltaTime;
            
            yield return 0;
        }
    }

    private void SpawnTrees()
    {
        Instantiate(tree, new Vector3(52, 0), Quaternion.identity, transform);
    }

    private void SpawnBones()
    {
        int[] spawnPoints = {-2, 2};
        
        Instantiate(bone, new Vector3(52, spawnPoints[Random.Range(0,2)]), Quaternion.identity, transform);
    }
}
