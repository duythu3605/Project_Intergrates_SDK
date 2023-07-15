using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager _uIManager;
    [HideInInspector]
    public DataBaseManager _dataBase;
    public AuthenManager _authenManager;
    private void Awake()
    {
        _uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _dataBase = GameObject.Find("DataBaseManager").GetComponent<DataBaseManager>();
        _authenManager = GameObject.Find("Authentication").GetComponent<AuthenManager>();

        _uIManager.Init(this);
        _dataBase.Init(this);
        _authenManager.Init(this);
    }
}
