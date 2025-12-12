using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private AudioSource _sfxSource;

    [Header("Clips")]
    public AudioClip buttonClickClip;
    public AudioClip plantClip;
    public AudioClip enemyHitClip; 
    public AudioClip meleeSwingClip;
    public AudioClip treeHurtClip;
    public AudioClip jumpClip;
    public AudioClip turretShotClip;
    public AudioClip magicShotClip;

    [Header("Game Settings")]
    public float matchDuration = 120f; 
    public float TimeRemaining { get; private set; }
    public int HighScore { get; private set; }
    public bool GameWon { get; private set; }

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

    void Update()
    {
        if (IsGameActive)
        {
            TimeRemaining -= Time.deltaTime;

            // CHECK WIN CONDITION (Time ran out)
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                LevelComplete();
            }
        }
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
        TimeRemaining = matchDuration; 
        IsGameActive = true;
        GameWon = false;

        // Load the saved high score (default to 0 if none exists)
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        Time.timeScale = 1; 
        SceneManager.LoadScene("GameScene"); 
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && _sfxSource != null)
        {
            
            _sfxSource.PlayOneShot(clip);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log("Current Score: " + Score);
    }

    private void LevelComplete()
    {
        GameWon = true;
        GameOver();
    }
    
    public void GameOver()
    {
        IsGameActive = false;
        Time.timeScale = 0;

        if (Score > HighScore)
        {
            HighScore = Score;
            // Save to disk so it remembers after you quit
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        Debug.Log("Game Over! Final Score: " + Score);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the built application
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}