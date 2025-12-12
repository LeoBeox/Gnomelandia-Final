using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // If the instance is null, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                // If it STILL doesn't exist (we are testing GameScene directly), create a temporary one
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager (Auto-Created)");
                    _instance = go.AddComponent<GameManager>();
                    
                    // Optional: Initialize default values for testing
                    _instance.InitializeForTesting(); 
                }
            }
            return _instance;
        }
    }
    public int Score { get; private set; }
    public bool IsGameActive { get; private set; }

    private void Awake()
    {
       
       // Singleton Logic
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // Destroy duplicates
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject); // Persist across scenes
    }

    private void InitializeForTesting()
    {
        Debug.LogWarning("GameManager was auto-created for testing!");
        Score = 0;
        IsGameActive = true;
        Time.timeScale = 1;
    }
    
    
    
    public void StartGame()
    {
        Score = 0;
        IsGameActive = true;
        Time.timeScale = 1; // Ensure time is running
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