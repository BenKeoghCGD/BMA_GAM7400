using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Difficulty Settings")]
    public float initialDifficulty = 1f;  // Starting difficulty
    public float difficultyIncreasePercentage = 10f;  // Percentage increase per interval
    public float timeToIncrease = 10f;  // Time in seconds to wait before increasing difficulty
    public int increaseThreshold = 5;  // Number of increases before moving to next stage

    private float currentDifficulty;
    private int increaseCount;
    private int currentStage;

    public float CurrentDifficulty
    {
        get { return currentDifficulty; }
    }
    private void Start()
    {
        currentDifficulty = initialDifficulty;
        increaseCount = 0;
        currentStage = 0;
        InvokeRepeating(nameof(IncreaseDifficulty), timeToIncrease, timeToIncrease);
    }

    private void IncreaseDifficulty()
    {
        // Increase the difficulty
        currentDifficulty += currentDifficulty * (difficultyIncreasePercentage / 100f);
        increaseCount++;

        // Check if we've reached the threshold for a stage increase
        if (increaseCount >= increaseThreshold)
        {
            currentStage++;
            increaseCount = 0;

            // Modify difficulty settings for the next stage
            AdjustForNextStage();
        }

        Debug.Log($"Current Difficulty: {currentDifficulty}, Current Stage: {currentStage}");
    }

    private void AdjustForNextStage()
    {
        // Increase difficulty and threshold for the next stage
        difficultyIncreasePercentage += 5f;  // Increase the percentage increment
        increaseThreshold += 3;  // Increase the number of increases required for the next stage
        // You can also adjust other variables here as needed
        // e.g. increase litter value or adjust game mechanics



        Debug.Log($"Stage {currentStage} Adjustments: Increase % to {difficultyIncreasePercentage}, Threshold to {increaseThreshold}");
    }


}
