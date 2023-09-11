using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class OnDifficultyChangedEventArgs : EventArgs
    {
        public Difficulty newDifficulty;
    }
    
    public static DifficultyManager Instance { get; private set; }

    public event EventHandler<OnDifficultyChangedEventArgs> OnDifficultyChanged;

    private Difficulty difficulty;
    private float timer;
    private float timerMax = 120f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are multiple Difficulty Managers!!!");
            Destroy(this);
        }
    }
    
    private void Update()
    {
        if (difficulty == Difficulty.Hard)
        {
            return;
        }
        
        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            timer = 0f;
            ChangeDifficulty();
        }
    }
    
    private void ChangeDifficulty()
    {
        if (difficulty == Difficulty.Easy)
        {
            difficulty = Difficulty.Medium;
            timerMax = 150f;
        }
        else if (difficulty == Difficulty.Medium)
        {
            difficulty = Difficulty.Hard;
        }
        
        OnDifficultyChanged?.Invoke(this, new OnDifficultyChangedEventArgs
        {
            newDifficulty = difficulty
        });
        
        Debug.Log($"Difficulty is {difficulty}");
    }
}
