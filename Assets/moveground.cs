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

    public float _birdSpawnRangeEnd;
    public float _birdSpawnRangeStart;
    
    public List<GameObject> obstacles;

    public void StartGame()
    {
        InvokeRepeating("SpawnTrees", 5f, 5f);
        InvokeRepeating("SpawnBones", 2f, 2f);

        // StartCoroutine(SpawnObstacle());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var groundCounter = 0;
        var obstacleCounter = 0;
        var cloudCounter = 0;
        var birdCounter = 0;
        
        foreach (Transform child in transform)
        {
            var currSpeed = speed;
                
                 if (child.CompareTag("Ground")) groundCounter++;
                 else if (child.CompareTag("Tree"))   currSpeed *= .9f;
                 else if (child.CompareTag("Obstacle")) obstacleCounter++;
                 else if (child.CompareTag("Bird"))   {currSpeed *= 1.2f; birdCounter++;}
                 else if (child.CompareTag("Cloud")){  currSpeed *= .8f; cloudCounter++;}

                 var body = child.GetComponent<Rigidbody2D>();
                 
            switch (GameManager.instance.gameStarted)
            {
                case false when child.CompareTag("Cloud") || child.CompareTag("Bird"):
                    body.position += new Vector2(-currSpeed * 0.2f, 0);
                    break;
                case true:
                    body.position += new Vector2(-currSpeed, 0);
                    break;
            }

        }

        if (groundCounter < 2)
        {
            Instantiate(ground, new Vector3(52, -5, 0), Quaternion.identity, transform);
        }

        if (obstacleCounter < 2)
        {
            var index = Random.Range(0, obstacles.Count);

            Instantiate(obstacles[index], new Vector3(52, 5), Quaternion.identity, transform);
        }

        if (birdCounter < 2)
        {
            Instantiate(bird, new Vector3(52, Random.Range(_birdSpawnRangeStart, _birdSpawnRangeEnd), -1479.249f), Quaternion.identity, transform);
        }
        
        if (cloudCounter < 2)
        {
            Instantiate(cloud, new Vector3(52, Random.Range(cloudSpawnRangeStart, cloudSpawnRangeEnd)), Quaternion.identity, transform);
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
