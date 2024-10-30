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
        int finalscore = PlayerPrefs.GetInt("FinalScore", 0);
        SetFinalScore(finalscore);

        PlayerPrefs.DeleteKey("FinalScore");
        LoadTopScores();
        UpdateScoreDisplay();
    }

    public void SetFinalScore(int finalScore)
    {
        
        finalScoreText.text = "Final Score: " + finalScore;

        AddScore(finalScore);
    }

    private void AddScore(int newScore)
    {
        // Add new score and sort the list in descending order and then Sorts highest to lowest, it ensures only top 3 scores remain in the list
        topScores.Add(newScore);
        topScores.Sort((a, b) => b.CompareTo(a)); 

        // 
        if (topScores.Count > 3) topScores.RemoveAt(3);

        SaveTopScores();
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
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
        topScores.Clear();
        for (int i = 0; i < 3; i++)
        {
            topScores.Add(PlayerPrefs.GetInt("TopScore" + i, 0));
        }
    }
}
