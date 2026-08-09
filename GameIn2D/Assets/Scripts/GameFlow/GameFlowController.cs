using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class GameFlowController : MonoBehaviour
{
    [SerializeField] private GameState currentState = GameState.Menu;

    private bool isPaused;

    public event Action<GameState, GameState> StateChanging;

    public GameState CurrentState => currentState;

    public void StartGame()
    {
        ChangeState(GameState.InGame);
    }

    public void RestartGame()
    {
        ChangeState(GameState.InGame);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
        isPaused = false;
    }

    public void BackToMenu()
    {
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ChangeState(GameState.Menu);
        }
    }

    public void BackToGame()
    {
        ChangeState(GameState.InGame);
    }

    public void PauseGame()
    {
        if (currentState != GameState.GameOver && currentState != GameState.Menu)
        {
            ChangeState(GameState.Pause);
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        ChangeState(GameState.InGame);
    }

    public void OpenMenuOptions()
    {
        ChangeState(GameState.Options);
    }

    private void ChangeState(GameState nextState)
    {
        StateChanging?.Invoke(currentState, nextState);

        if (nextState == GameState.Menu || nextState == GameState.InGame)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        currentState = nextState;

        if (nextState == GameState.InGame)
        {
            isPaused = true;
        }
    }
}
