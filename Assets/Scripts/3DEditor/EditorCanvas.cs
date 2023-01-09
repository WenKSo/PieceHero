using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using FusionUtilsEvents;
using TMPro;
using UnityEngine.SceneManagement;


public class EditorCanvas : MonoBehaviour
{
    private GameMode _gameMode;

    public string Nickname = "Player";
    public GameLauncher Launcher;

    public FusionEvent OnPlayerJoinedEvent;
    public FusionEvent OnPlayerLeftEvent;
    public FusionEvent OnShutdownEvent;
    public FusionEvent OnPlayerDataSpawnedEvent;

    [Space]
    [SerializeField] private GameObject _initPanel;
    [SerializeField] private GameObject _modePanel;
    [SerializeField] private GameObject _lobbyPanel;
    [Space]
    [SerializeField] private GameObject _infoEnter;
    [SerializeField] private GameObject _chooseMode;
    [Space]
    [SerializeField] private TMP_InputField _nickname;
    [SerializeField] private TMP_InputField _room;

    public void RunButton()
    {
        _initPanel.SetActive(false);
        _modePanel.SetActive(true);
    }

    public void InfoEnter()
    {
        _chooseMode.SetActive(false);
        _infoEnter.SetActive(true);
    }

    public void Back()
    {
        _initPanel.SetActive(true);
        _modePanel.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene (sceneName:"3DTest");
    }

    //Called from button
    public void StartLauncher()
    {
        Launcher = FindObjectOfType<GameLauncher>();
        Nickname = _nickname.text;
        PlayerPrefs.SetString("Nick", Nickname);
        Launcher.Launch(_gameMode, _room.text);
        _nickname.transform.parent.gameObject.SetActive(false);
    }

    //Called from button
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
