using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI scoreTextGameOver;
    
    public bool gameStarted;

    private int lastMulti;
    
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

        var speedMulti = _score / 30;
        if (speedMulti - lastMulti != 0) Debug.Log(speedMulti - lastMulti);
        
        groundMover.speed += 0.05f * (speedMulti - lastMulti);

        lastMulti = speedMulti;
        
        _scoreText.text = "Score: " + _score;
    }
    
    public void GameStart()
    {
        Time.timeScale = 1f;
        gameStart.SetActive(false);

        gameStarted = true;

        groundMover.StartGame();
        
        DogAnimator.SetBool("GameStarted", true);
        
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        game.SetActive(false);
        gameOver.SetActive(true);
        scoreTextGameOver.text = "Score: " + _score;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
