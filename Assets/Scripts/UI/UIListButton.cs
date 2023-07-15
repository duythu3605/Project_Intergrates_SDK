using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListButton : MonoBehaviour
{
    private UIManager _uIManager;
    [Header("List Button FireBase")]
    public Button _btnRealTime;
    public Button _btnAuthentication;

    public void Init(UIManager uIManager)
    {
        _uIManager = uIManager;

        _btnRealTime.onClick.AddListener(() => 
        {
            _uIManager._uIRealTime.ShowUIRealTime();
            HideUIListButton();
        });
        _btnAuthentication.onClick.AddListener(() => 
        {
            _uIManager._uiLogin.ShowUILogin();
            HideUIListButton(); 
        });
    }
    public void ShowUIListButton()
    {
        gameObject.SetActive(true);
    }
    public void HideUIListButton()
    {
        gameObject.SetActive(false);
    }
}
