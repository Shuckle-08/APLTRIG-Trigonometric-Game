using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    private int lastMilestone = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (score >= lastMilestone + 5)
        {
            lastMilestone = score - (score % 5);
            FindObjectOfType<ButtonDirectionRandomizer>().Reshuffle();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}
