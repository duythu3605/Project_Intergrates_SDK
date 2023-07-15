using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRegister : MonoBehaviour
{
    private GameManager _gameManager;

    [Header("Input")]
    public TMP_InputField _inputName;
    public TMP_InputField _inputEmail;
    public TMP_InputField _inputPass;
    public TMP_InputField _inputPassAgain;
    public TMP_Text _confirmText;

    [Header("Button")]
    public Button _btnRegister;
    public Button _btnBack;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnRegister.onClick.AddListener(() => 
        {
            _gameManager._authenManager.RegisterClick(_inputEmail.text, _inputPass.text, _inputName.text);

        });

        _btnBack.onClick.AddListener(() => 
        {
            HideUIRegister();
            _gameManager._uIManager._uiLogin.ShowUILogin();
        });
    }

    public void ShowUIRegister()
    {
        gameObject.SetActive(true);
    }
    public void HideUIRegister()
    {
        gameObject.SetActive(false);
    }
    public void ShowConfirmText(string value, bool isWarning)
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
