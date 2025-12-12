using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; private set; }
    public bool IsGameActive { get; private set; }

    private void Awake()
    {
        // Singleton Logic
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // Destroy duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Persist across scenes
    }

    public void StartGame()
    {
        Score = 0;
        IsGameActive = true;
        Time.timeScale = Time.deltaTime; // Ensure time is running
        SceneManager.LoadScene("GameScene"); // Load the actual level
    }

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log("Current Score: " + Score);
    }

    public void GameOver()
    {
        IsGameActive = false;
        Time.timeScale = 0; 
        Debug.Log("Game Over!");
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the built application
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}