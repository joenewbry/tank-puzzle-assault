using System;
using UnityEngine;

public enum GamePhase {
    Boot,
    Playing,
    Win,
    Lose
}

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }

    public GamePhase CurrentPhase { get; private set; } = GamePhase.Boot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        CurrentPhase = GamePhase.Playing;
        Debug.Log("Game started.");
    }

    public void EndGame(GamePhase result)
    {
        CurrentPhase = result;
        Debug.Log(result == GamePhase.Win ? "Player won!" : "Player lost.");
    }

    public void ResetGame()
    {
        CurrentPhase = GamePhase.Boot;
        Debug.Log("Game reset.");
    }
}