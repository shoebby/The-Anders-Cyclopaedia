using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IObserver
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

    #region Observer Pattern Stuff

    [SerializeField] Subject sampleSubject;

    public void OnNotify()
    {
        throw new System.NotImplementedException();
        //do something idk how this design pattern works
    }

    private void OnEnable() => sampleSubject.AddObserver(this);
    private void OnDisable() => sampleSubject.RemoveObserver(this);

    #endregion
}
