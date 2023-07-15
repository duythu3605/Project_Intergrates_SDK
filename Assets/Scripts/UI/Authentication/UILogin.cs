using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILogin : MonoBehaviour
{
    private GameManager _gameManager;

    [Header("Box Login")]
    public TMP_InputField _inputEmail;
    public TMP_InputField _inputPass;
    public Button _btnLogin;
    public Button _btnRegister;
    public TMP_Text _confirmText;

    [Header("Bottom")]
    public Button _btnBack;
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;


        _btnLogin.onClick.AddListener(() => 
        {
            _gameManager._authenManager.LoginClick(_inputEmail.text, _inputPass.text);
        });

        _btnRegister.onClick.AddListener(() => 
        {
            _gameManager._uIManager._uiRegister.ShowUIRegister();
            HideUILogin();
        });
        _btnBack.onClick.AddListener(() => 
        {
            _gameManager._uIManager._uiListButton.ShowUIListButton();
            HideUILogin();
        });
    }
    public void ShowUILogin()
    {
        gameObject.SetActive(true);
    }
    public void HideUILogin()
    {
        gameObject.SetActive(false);
    }

    public void ShowConfirmText(string value,bool isWarning)
    {
        _confirmText.enabled = true;
        if (isWarning)
        {
            _confirmText.color = Color.red;
        }
        else
        {
            _confirmText.color = Color.green;
        }
        _confirmText.text = value;
        StartCoroutine(TimeHideText());
    }

    private IEnumerator TimeHideText()
    {
        yield return new WaitForSeconds(1.5f);
        _confirmText.enabled = false;
    }
}
