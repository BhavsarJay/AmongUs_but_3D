using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.PlayerLoop;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public static PhotonRoom room;
    public PhotonView PV;

    private int currentSceneIndex;
    public int LobbySceneIndex;
    public int GameSceneIndex;

    public int PlayersCount;
    public int ImpostersCount = 1;
    
    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if (PhotonRoom.room != this)
            {
                Debug.Log(room.name);
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        PlayersCount = PhotonNetwork.PlayerList.Length;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("PhotonRoom: Room Joined, Name = " + PhotonNetwork.CurrentRoom.Name);
        LoadLobbyScene();
    }

    public void OnClick_StartBtn() => LoadGameScene();

    private void LoadLobbyScene()
    {
        // Only Host(MasterClient) should change scenes
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(LobbySceneIndex);
    }

    private void LoadGameScene()
    {
        // Only Host(MasterClient) should change scenes
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(GameSceneIndex);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;
        if (currentSceneIndex == LobbySceneIndex)
        {
            SpawnPlayer("Player");
        }
    }

    private void SpawnPlayer(string name)
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", name), Vector3.zero, Quaternion.identity);
    }
}