using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _scoreText;
   [SerializeField] private TextMeshProUGUI _treeHealthText;
   [SerializeField] private Health _tree;
   [SerializeField] private TextMeshProUGUI _timerText;


    void Update()
    {
        
        if (GameManager.Instance != null)
        {
        
            _scoreText.text = "Gnoints: " + GameManager.Instance.Score;
            _treeHealthText.text = "Tree Health: " + _tree.CurrentHealth;

            if (_timerText != null)
            {
                float time = GameManager.Instance.TimeRemaining;
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                
                // Formatting thank you gemini
                _timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        }
    }
}