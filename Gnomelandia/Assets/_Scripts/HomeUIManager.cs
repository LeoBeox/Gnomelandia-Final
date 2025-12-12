using UnityEngine;

public class HomeScreenUI : MonoBehaviour
{

    public void OnStartButtonClicked()
    {

        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            Debug.LogError("GameManager instance not found! Can't start.");
        }
    }

    public void OnQuitButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            Debug.LogError("GameManager instance not found! Can't quit.");
        }
    }
}