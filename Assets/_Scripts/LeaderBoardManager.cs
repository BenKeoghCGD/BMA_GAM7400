using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private List<TextMeshProUGUI> topScoreTexts;
    private List<int> topScores = new List<int>();

    private void Start()
    {
        // Retrieve and display the final score
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        SetFinalScore(finalScore);

        // Load previously saved top scores
        LoadTopScores();

        // If the final score qualifies as a top score, add it
        AddScore(finalScore);

        // Update the display to show the latest scores
        UpdateScoreDisplay();

        // Clear the temporary final score
        PlayerPrefs.DeleteKey("FinalScore");
    }

    public void SetFinalScore(int finalScore)
    {
        finalScoreText.text = "Final Score: " + finalScore;
    }

    private void AddScore(int newScore)
    {
        // Add new score only if it qualifies as a top score or if there are fewer than 3 top scores
        if (topScores.Count < 3 || newScore > topScores[topScores.Count - 1])
        {
            topScores.Add(newScore);
            topScores.Sort((a, b) => b.CompareTo(a)); // Sort in descending order

            // Ensure only the top 3 scores are kept
            if (topScores.Count > 3)
            {
                topScores.RemoveAt(3);
            }

            // Save updated top scores
            SaveTopScores();
        }
    }

    private void UpdateScoreDisplay()
    {
        // Display top scores in the UI
        for (int i = 0; i < topScoreTexts.Count; i++)
        {
            topScoreTexts[i].text = i < topScores.Count ? "Top " + (i + 1) + ": " + topScores[i].ToString() : "-";
        }
    }

    private void SaveTopScores()
    {
        for (int i = 0; i < topScores.Count; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, topScores[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadTopScores()
    {
        // Clear the list to ensure no duplicate entries
        topScores.Clear();

        // Load the top scores from PlayerPrefs
        for (int i = 0; i < 3; i++)
        {
            int score = PlayerPrefs.GetInt("TopScore" + i, 0);
            if (score > 0) topScores.Add(score);
        }

        // Sort the loaded scores to ensure descending order
        topScores.Sort((a, b) => b.CompareTo(a));
    }
}

