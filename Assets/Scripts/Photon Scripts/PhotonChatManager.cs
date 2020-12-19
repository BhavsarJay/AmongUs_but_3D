using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;

    public GameObject ChatGUI;
    public TMP_InputField inputField;
    public TextMeshProUGUI chatRoom_txtBox;

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        chatClient.Subscribe("AlivePlayers");
    }

    public void OnDisconnected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}{1}: {2}", msgs, senders[i], messages[i]);
        }

        chatRoom_txtBox.text += '\n' + msgs;

        // All public messages are automatically cached in `Dictionary<string, ChatChannel> PublicChannels`.
        // So you don't have to keep track of them.
        // The channel name is the key for `PublicChannels`.
        // In very long or active conversations, you might want to trim each channels history.
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }



    void Start()
    {
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, 
                            PhotonNetwork.AppVersion, 
                            new AuthenticationValues(PlayerPrefs.GetString("PlayerName")));
    }

    void Update()
    {
        chatClient.Service();
    }


    bool chatGUI_isOpen = false;
    public void ToggleChatGUI()
    {
        if (!chatGUI_isOpen)
        {
            ChatGUI.SetActive(true);
            chatGUI_isOpen = true;
        }
        else
        {
            ChatGUI.SetActive(false);
            chatGUI_isOpen = false;
        }
    }

    public void SendMessage()
    {
        string message = inputField.text;
        message = message.Trim();
        if (message.Length <= 1)
            return;

        chatClient.PublishMessage("AlivePlayers", message);
        
        inputField.text = string.Empty;
    }

}
