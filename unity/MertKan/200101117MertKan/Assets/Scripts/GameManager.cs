using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text highScoreText;
    public Button restartButton; // Restart butonu için referans
    public Button returnToMenuButton; // Ana menü butonu için referans
    private bool isGameOver = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        // Butonların onClick eventlerini ayarlama
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMenu);
        }
    }

    public void GameOver(int finalScore)
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over! Final Score: " + finalScore);

        // Mevcut skoru ve yüksek skoru güncelle
        scoreText.text = "Score: " + finalScore;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreText.text = "High Score: " + highScore;

        // Game Over panelini göster
        gameOverPanel.SetActive(true);

        // ObstacleSpawner'ı durdur
        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        // Timer'ı durdur
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StopTimer();
        }
    }

    public void RestartGame()
    {
        // "Game1" sahnesini yeniden yükle
        SceneManager.LoadScene("Game1");
    }

    public void ReturnToMenu()
    {
        // Ana menüye dön
        SceneManager.LoadScene("MainMenu");
    }
}
