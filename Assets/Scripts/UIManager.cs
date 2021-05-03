using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Sprite emptyLifeSprite;
    [SerializeField] private Image[] livesImage;

    private GameManager _gameManager;

   private void Start()
    {
        scoreText.text = "Score: " + 0;
        _gameManager = GameObject.FindWithTag("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null) Debug.LogError("Game Manager is NULL!");
    }

   public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
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