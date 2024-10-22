using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI[] topScoreTexts = new TextMeshProUGUI[3];

    [SerializeField] private ScoreManager scoreManager;

    private int finalScore;
    private List<int> topScores = new List<int>() { 0, 0, 0 };


    private void Start()
    {
        LoadScores();
        DisplayScores();
    }

    public void SetFinalScore()
    {
       finalScore = scoreManager.totalScore;
        Debug.Log("final Score Set" +  finalScore);
        UpdateLeaderboard(finalScore);
    }

    private void UpdateLeaderboard(int finalScore)
    {
        bool scoreInserted = false;
        //loops through the top scores to find where the new score fits
        for (int i = 0; i < topScores.Count; i++)
        {
            if (finalScore > topScores[i]) 
            {
                topScores.Insert(i, finalScore);
                scoreInserted = true;
                break;
            }
        }

        if (!scoreInserted && topScores.Count < 3)
        {
            topScores.Add(finalScore);
        }
        
        if (topScores.Count > 3)
        {
            topScores.RemoveAt(topScores.Count - 1);
        }
        topScores.Sort((a, b) => b.CompareTo(a));

        SaveScores();
        DisplayScores();
        
    }

    private void LoadScores()
    {
        // loads the top 3 scores from PlayerPrefs defaults to 0 if theres no score
        for (int i = 0; i < topScores.Count; i++)
        {
            topScores[i] = PlayerPrefs.GetInt("TopScore" + i, 0);
        }
    }

    private void SaveScores()
    {
        // saves scores in PlayerPrefs
        for (int i = 0; i < topScores.Count; i++)
        {
            PlayerPrefs.SetInt("TopScore" + i, topScores[i]);
        }
        PlayerPrefs.Save(); //save data
    }

    private void DisplayScores()
    {
        finalScoreText.text = "Final Score" + ":" + finalScore.ToString(); //displays final score on screen

        // displays top 3 scores on screen that are ranked 1 to 3
        for (int i = 0; i < topScoreTexts.Length; i++)
        {
            topScoreTexts[i].text = (i + 1) + ":" + (i < topScores.Count ? topScores[i].ToString() : "0");
        }
    }
}
