using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _scoreText;
   [SerializeField] private TextMeshProUGUI _treeHealthText;
   [SerializeField] private Health _tree;


    void Update()
    {
        // Safety check: Ensure the GameManager exists
        if (GameManager.Instance != null)
        {
            // Ask the Singleton for the current Score
            _scoreText.text = "Gnoints: " + GameManager.Instance.Score;
            _treeHealthText.text = "Tree Health: " + _tree.CurrentHealth;
        }
    }
}