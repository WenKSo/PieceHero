using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using FusionUtilsEvents;
using UnityEngine.SceneManagement;


public class EditorCanvas : MonoBehaviour
{
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
}
