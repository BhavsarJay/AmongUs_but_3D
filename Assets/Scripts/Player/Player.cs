using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
//mini
public class Player : MonoBehaviourPunCallbacks
{
    private PhotonView PV;

    public delegate void OnReady();
    public static event OnReady OnPlayersLoadedInGameScene;

    public static event Action OnThisPlayerDead;

    public enum State { alive, dead };
    public State myState = State.alive;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;


        //Register player
        string id = GetComponent<PhotonView>().ViewID.ToString();
        PlayersList.RegisterPlayer(id, gameObject);

        OnThisPlayerDead += Player_OnThisPlayerDead;
    }

    private void Player_OnThisPlayerDead()
    {
        myState = State.dead;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;


        //Unregister player
        PlayersList.UnRegisterPlayer(transform.name);

        OnThisPlayerDead -= Player_OnThisPlayerDead;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        PV = GetComponent<PhotonView>();
        if (scene.buildIndex == PhotonRoom.room.GameSceneIndex)
            StartCoroutine(WaitForPlayersToJoin());
    }

    IEnumerator WaitForPlayersToJoin()
    {
        GameObject[] players;
        int playersCount = 0;

        while (playersCount != PhotonRoom.room.PlayersCount)
        {
            yield return new WaitForSeconds(0.5f);
            players = GameObject.FindGameObjectsWithTag("Player");
            playersCount = players.Length;
        }

        //Fire an event that everyone is ready only from local player or else multiple events will be fired.
        if (PV.IsMine)
            OnPlayersLoadedInGameScene?.Invoke();

    }

    public void ThisPlayerDead()
    {
        OnThisPlayerDead?.Invoke();
    }
}