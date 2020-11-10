using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    private void Awake()
    {
        lobby = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClick_JoinBtn()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnClick_CreateBtn()
    {
        CreateRoom();
    }


    void CreateRoom()
    {
        Debug.Log("Creating Room....");
        int randNo = Random.Range(0, 1000);
        RoomOptions roomOps = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom("Room_" + randNo, roomOps);
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Server.");
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join");
        CreateRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Create Room");
        CreateRoom();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }
}
