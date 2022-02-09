using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score;

    public GameObject gameOver;
    public GameObject gameStart;
    public GameObject game;

    public moveground groundMover;
    
    public Animator DogAnimator;
    
    public TextMeshProUGUI scoreText;

    public bool gameStarted = false;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = "Score: " + (int) score;
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        gameStart.SetActive(false);

        gameStarted = true;

        groundMover.StartGame();
        
        DogAnimator.SetBool("GameStarted", true);
        DogAnimator.SetTrigger("Landed");
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        game.SetActive(false);
        gameOver.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
