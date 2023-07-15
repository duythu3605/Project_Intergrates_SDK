using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using TMPro;

public class DataBaseManager : MonoBehaviour
{
    private string userId;
    private DatabaseReference dbReference;
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        userId = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser(string name, string gold)
    {
        User newUser = new User(name, int.Parse(gold));
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    public IEnumerator GetName(Action<string> onCallBack)
    {
        var userNameData = dbReference.Child("users").Child(userId).Child("_name").GetValueAsync();
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack.Invoke(snapshot.Value.ToString());
        }
    }
    public IEnumerator GetGold(Action<int> onCallBack)
    {
        var userGoldData = dbReference.Child("users").Child(userId).Child("_gold").GetValueAsync();
        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;
            onCallBack.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) => 
        {
            _gameManager._uIManager._uIRealTime._nameText.text = name;
        }));
        StartCoroutine(GetGold((int gold) =>
        {
            _gameManager._uIManager._uIRealTime._goldText.text =gold.ToString();
        }));
    }

    public void UpdateName(string name)
    {
        dbReference.Child("users").Child(userId).Child("_name").SetValueAsync(name);
    }
    public void UpdateGold(string gold)
    {
        dbReference.Child("users").Child(userId).Child("_gold").SetValueAsync(gold);
    }
}
