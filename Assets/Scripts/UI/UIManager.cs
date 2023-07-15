using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    [Header("UI")]
    public UIListButton _uiListButton;
    public UIRealTime _uIRealTime;
    public UILogin _uiLogin;
    public UIRegister _uiRegister;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _uiListButton.Init(this);
        _uIRealTime.Init(_gameManager);
        _uiLogin.Init(_gameManager);
        _uiRegister.Init(_gameManager);
        ShowFirstUIListButton();
    }
    private void ShowFirstUIListButton()
    {
        _uiListButton.ShowUIListButton();
        _uIRealTime.HideUIRealTime();
        _uiLogin.HideUILogin();
        _uiRegister.HideUIRegister();
    }
}
