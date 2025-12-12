using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class GameMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject menuPanel;      
    public TextMeshProUGUI titleText; 

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance.IsGameActive)
            {
                TogglePause();
            }
        }

        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive)
        {
            if (!menuPanel.activeSelf)
            {
                ShowGameOver();
            }
        }
    }

    public void TogglePause()
    {
        // Check if the panel is currently on or off
        bool isCurrentlyPaused = menuPanel.activeSelf;

        if (isCurrentlyPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        menuPanel.SetActive(true);
        titleText.text = "PAUSED";
        
        Time.timeScale = 0f; // FREEZE TIME
        
        // Unlock the cursor so you can click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        menuPanel.SetActive(false);
        
        Time.timeScale = 1f; // UNFREEZE TIME
        
        // Lock the cursor back to the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowGameOver()
    {
        menuPanel.SetActive(true);
        titleText.text = "GAME OVER";
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void OnRestartClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            Debug.Log("Error bruh");
        }
    }

    public void OnQuitClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }

    public void PlayButtonSound()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.PlaySFX(GameManager.Instance.buttonClickClip);
    }
}