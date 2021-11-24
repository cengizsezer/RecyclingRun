using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public bool IsGameStarted = false;
    public bool IsGameFinished = false;
    public bool AutoStart = true;
    public bool _allowInputOnlyInGameplay = false;
    public LeanTouch _inputController;

    public readonly UnityEvent OnGameStarted = new UnityEvent();
    public readonly UnityEvent OnGameWin = new UnityEvent();
    public readonly UnityEvent OnGameLost = new UnityEvent();
    public readonly UnityEvent OnNewLevelLoaded = new UnityEvent();
    public readonly UnityEvent OnGameRestarted = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();


        _inputController = LeanTouch.Instance;

        if (!_inputController)
        {
            Debug.LogError("There is no lean touch in the scene!");
# if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        if (_allowInputOnlyInGameplay)
        {
            _inputController.enabled = false;
        }

    }

    private void Start()
    {

        if (AutoStart)
        {

            StartGame();
        }
        else
        {
            IsGameFinished = false;
        }


    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !IsGameStarted && !AutoStart && IsGameFinished == false)
        {
            StartGame();
        }

    }

    public virtual void StartGame()
    {
        if (IsGameStarted)
        {
            Debug.LogError("Game is already started. You can not start the game after it is started already");
            return;
        }

        OnGameStarted.Invoke();
        IsGameStarted = true;
        if (_allowInputOnlyInGameplay) _inputController.enabled = true;
    }

    public virtual void WinGame()
    {
        if (!IsGameStarted)
        {
            Debug.LogError("Game Win");
            return;
        }
        OnGameWin.Invoke();
        IsGameStarted = false;
        IsGameFinished = true;
        if (_allowInputOnlyInGameplay) _inputController.enabled = false;

    }

    public virtual void LostGame()
    {
        if (!IsGameStarted)
        {
            Debug.LogError("Game Lost");
            return;
        }
        IsGameStarted = false;
        IsGameFinished = true;
        OnGameLost.Invoke();
        if (_allowInputOnlyInGameplay) _inputController.enabled = false;

    }

    public virtual void ReloadLevel()
    {

        if (IsGameStarted)
        {
            Debug.LogError("Gameplay is continuing You can not increase level during a gameplay");
            return;
        }

        SceneManager.LoadScene(0);

    }

    public virtual void IncreaseLevel()
    {

        if (IsGameStarted)
        {
            Debug.LogError("Gameplay is continuing You can not increase level during a gameplay");
            return;
        }
        SceneManager.LoadScene(0);
    }

}
