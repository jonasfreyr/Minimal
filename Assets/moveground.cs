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
    
    public int cloudSpawnRangeStart;
    public int cloudSpawnRangeEnd;
    public float cloudSpawnRate;
    
    public List<GameObject> obstacles;

    private float _obstacleSpawnRate;

    public void StartGame()
    {
        _obstacleSpawnRate = 0;
        _obstacleSpawnRate = 1.5f;
        
        InvokeRepeating("SpawnClouds", 1f, cloudSpawnRate);
        InvokeRepeating("SpawnBirds", 1f, cloudSpawnRate);
        InvokeRepeating("SpawnTrees", 5f, cloudSpawnRate);

        StartCoroutine(SpawnObstacle());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!GameManager.instance.gameStarted) return;
        var groundCounter = 0;

        foreach (Transform child in transform)
        {
            var currSpeed = speed;
                
            if (child.CompareTag("Ground")) groundCounter++;
            else if (child.CompareTag("Bird")) currSpeed *= 1.2f;
            else if (child.CompareTag("Tree")) currSpeed *= .9f;
            else if (child.CompareTag("Cloud")) currSpeed *= .8f;

            child.position += new Vector3(-currSpeed, 0, 0);
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

                int index = Random.Range(0, obstacles.Count);

                Instantiate(obstacles[index], new Vector3(52, 5), Quaternion.identity, transform);
              
            }

            currentTime += Time.deltaTime;
            
            yield return 0;
        }
    }
    
    private void SpawnBirds()
    {
        Instantiate(bird, new Vector3(52, Random.Range(cloudSpawnRangeStart, cloudSpawnRangeEnd), -1479.249f), Quaternion.identity, transform);
    }
    
    private void SpawnClouds()
    {
        Instantiate(cloud, new Vector3(52, Random.Range(cloudSpawnRangeStart, cloudSpawnRangeEnd)), Quaternion.identity, transform);
    }
    
    private void SpawnTrees()
    {
        Instantiate(tree, new Vector3(52, 0), Quaternion.identity, transform);
    }
    
}
