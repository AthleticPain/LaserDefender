using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSession : MonoBehaviour
{

    int score = 0;

   private void Awake()
    {
        SetUpSingleton();       
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
