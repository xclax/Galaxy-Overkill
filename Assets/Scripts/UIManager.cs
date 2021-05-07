using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    private int _highScore;
    [SerializeField] private Sprite emptyLifeSprite;
    [SerializeField] private Image[] livesImage;

    private GameManager _gameManager;

   private void Start()
   {
       _highScore = PlayerPrefs.GetInt("highScore", 0);
       highScoreText.text = "Best: " + _highScore;
       scoreText.text = "Score: " + 0;
        
       _gameManager = GameObject.FindWithTag("Game_Manager").GetComponent<GameManager>();
       if (_gameManager == null) Debug.LogError("Game Manager is NULL!");
    }

   public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
       
    }

   private void UpdateBestScore(int highScore)
   {
       highScoreText.text = "Best: " + highScore;
   }
   

   public void CheckForBestScore(int playerScore)
   {
       if (playerScore > _highScore)
       {
           _highScore = playerScore;
           PlayerPrefs.SetInt("highScore", _highScore);
       }
       
       UpdateBestScore(_highScore);
       
   }
   

    public void UpdateLives(int currentLives)
    {
        livesImage[currentLives].sprite = emptyLifeSprite;

        if (currentLives == 0) StartGameOverSequence();
    }

    private void StartGameOverSequence()
    {
        _gameManager.GameOver();
    }

    public void ResumePlay()
    {
        _gameManager.ClosePauseMenu();
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}