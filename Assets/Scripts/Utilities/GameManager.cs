using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MainMenu,
        PauseMenu,
        GameWorld
    }

    private GameState currentGameState;

    public void SetGameState(GameState state)
    {
        currentGameState = state;
    }

    public void Pause() => Time.timeScale = 0f;
    public void Unpause() => Time.timeScale = 1f;
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void QuitGame() => Application.Quit();
}
