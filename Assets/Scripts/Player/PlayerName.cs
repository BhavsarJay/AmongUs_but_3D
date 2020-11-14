using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInpField = null;

    private const string PlayerPrefsNameKey = "PlayerName";

    void Start() => SetUpPlayerName();

    private void SetUpPlayerName()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
            return;

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        playerNameInpField.text = defaultName;
    }

    public void SavePlayerName()
    {
        string playername = playerNameInpField.text;
        PhotonNetwork.NickName = playername;
        PlayerPrefs.SetString(PlayerPrefsNameKey, playername);
    }
}
