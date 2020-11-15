using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoleManager : MonoBehaviourPunCallbacks
{
    [HideInInspector]public const string CREWMATE = "CREWMATE";
    [HideInInspector]public const string IMPOSTER = "IMPOSTER";
    private int[] impostersIndex;
    private int impostersCount;
    public GameObject[] players;

    private GameObject canvas;
    private GameObject CrewmateHud;
    private GameObject ImposterHud;
    private PhotonView PV;

    public string myrole;
    
    Dictionary<string, string> playersRole = new Dictionary<string, string>();



    public override void OnEnable() => Player.OnPlayersLoadedInGameScene += OnPlayersReady;
    public override void OnDisable() => Player.OnPlayersLoadedInGameScene -= OnPlayersReady;


    private void OnPlayersReady()
    {
        players = PlayersList.GetPlayers();
        //Register players and give roles
        if (PhotonNetwork.IsMasterClient)
        {
            RegisterPlayers();
        }
    }
    
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        canvas = FindObjectOfType<Canvas>().gameObject;
        CrewmateHud = canvas.transform.GetChild(0).gameObject;
        ImposterHud = canvas.transform.GetChild(1).gameObject;
    }

    private void RegisterPlayers()
    {
        impostersIndex = new int[PhotonRoom.room.ImpostersCount];
        int impostersCount = PhotonRoom.room.ImpostersCount;

        int playersCount = players.Length;
        for (int i = 0; i < impostersCount; i++)
            impostersIndex[i] = Random.Range(0, playersCount);

        impostersIndex[0] = 0; //To be imposter for testing.
        for (int i = 0; i < playersCount; i++)
        {
            string PlayerName = players[i].name;
            string PlayerRole;
            if (impostersIndex.Contains(i))
                PlayerRole = IMPOSTER;
            else
                PlayerRole = CREWMATE;

            playersRole.Add(PlayerName, PlayerRole);

            // IF local player then set myrole value.
            if (LayerMask.LayerToName(players[i].layer) == "LocalPlayer")
                myrole = PlayerRole;
        }

        PV.RPC("GetRoles", RpcTarget.AllBuffered, playersRole);
    }


    [PunRPC]
    void GetRoles(Dictionary<string, string> _playersRole)
    {
        playersRole = _playersRole;

        string myname = PlayersList.GetMyPlayer().name;
        myrole = playersRole[myname];
        
        //Reflect the role in UI
        if (myrole == IMPOSTER)
        {
            ShowImpostersHUD();
        }
        else if (myrole == CREWMATE)
        {
            ShowCrewmatesHUD();
        }
    }
    
    public string GetRole(string name)
    {
        return playersRole[name];
    }

    private void ShowImpostersHUD()
    {
        CrewmateHud.SetActive(false);
        ImposterHud.SetActive(true);
    }

    private void ShowCrewmatesHUD()
    {
        ImposterHud.SetActive(false);
        CrewmateHud.SetActive(true);
    }

}