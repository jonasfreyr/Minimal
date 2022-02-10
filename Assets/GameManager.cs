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
    private int _score;

    public GameObject gameOver;
    public GameObject gameStart;
    public GameObject game;

    public moveground groundMover;
    
    public Animator DogAnimator;
    
    public TextMeshProUGUI _scoreText;

    public bool gameStarted;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _score = 0;
        _scoreText.text = "Score: " + _score;
    }
    

    public void IncrementScore(int amount)
    {
        _score += amount;

        if (_score % 30 == 0 && _score >= 30)
        {
            groundMover.speed += 0.01f;
            
            Debug.Log("yo");
        }
        _scoreText.text = "Score: " + _score;
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
