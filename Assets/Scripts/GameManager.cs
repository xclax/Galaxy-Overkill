using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isGameOver;

    [SerializeField] private GameObject pauseMenuPanel;

    private void Update()
    {
        if (_isGameOver)
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
           pauseMenuPanel.SetActive(true);
           Time.timeScale = 0;
        }
    }


    public void ClosePauseMenu()
    {
        pauseMenuPanel.SetActive(false);
    }


    public void GameOver()
    {
        _isGameOver = true;
    }
    
}
