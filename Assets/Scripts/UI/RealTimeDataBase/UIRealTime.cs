using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIRealTime : MonoBehaviour
{
    [Header("Top")]
    public Button _btnBack;

    [Header("Middle")]
    public TMP_InputField _nameInput;
    public TMP_InputField _goldInput;

    public Button _btnSaveData;
    public Button _btnUpdateData;
    public Button _btnGetValue;

    public TMP_Text _nameText;
    public TMP_Text _goldText;
    public TMP_Text _noticeText;

    private GameManager _gameManager;


    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;

        _btnSaveData.onClick.AddListener(() => 
        {
            if(_nameInput.text != null && _goldInput.text != null)
            {
                SaveData();
                _noticeText.GetComponent<TMP_Text>().enabled = true;
                _noticeText.color = Color.green;
                _noticeText.text = "Saved!";
                StartCoroutine(TimeHideText());
            }
            if (_nameInput.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text == null || _goldInput.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text == null)
            {
                _noticeText.GetComponent<TMP_Text>().enabled = true;
                _noticeText.color = Color.red;
                _noticeText.text = "Check Space!";
                StartCoroutine(TimeHideText());
            }
        });

        _btnUpdateData.onClick.AddListener(() => 
        {
            _gameManager._dataBase.UpdateName(_nameInput.text);
            _gameManager._dataBase.UpdateGold(_goldInput.text);
            _noticeText.GetComponent<TMP_Text>().enabled = true;
            _noticeText.color = Color.green;
            _noticeText.text = "Updated!";
            StartCoroutine(TimeHideText());
        });
        _btnGetValue.onClick.AddListener(() => 
        {
            _gameManager._dataBase.GetUserInfo();
            _noticeText.GetComponent<TMP_Text>().enabled = true;
            _noticeText.color = Color.green;
            _noticeText.text = "Load Data!";
            StartCoroutine(TimeHideText());
        });
        

        _btnBack.onClick.AddListener(() => 
        {
            HideUIRealTime();
            gameManager._uIManager._uiListButton.ShowUIListButton();
        });
    }

    private IEnumerator TimeHideText()
    {
        yield return new WaitForSeconds(1.5f);
        _noticeText.GetComponent<TMP_Text>().enabled = false;
    }

    public void SaveData()
    {
        _gameManager._dataBase.CreateUser(_nameInput.text, _goldInput.text);
    }


    public void ShowUIRealTime()
    {
        gameObject.SetActive(true);
    }
    public void HideUIRealTime()
    {
        gameObject.SetActive(false);
    }
}
