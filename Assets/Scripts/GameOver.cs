using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    
    [SerializeField] private Text gameOverText;


    private void Start()
    {
        StartCoroutine(GameOverFlickerRoutine());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1); // Main Game Scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0); // Main Menu Scene
    }
  

    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
