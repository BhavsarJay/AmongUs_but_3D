using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillTrigger : MonoBehaviour
{
    
    RoleManager roleManager;
    PhotonView PV;
    PlayerProximity playerProximity;
    private string myrole;
    
    [Range(0.5f, 5f)]
    public float killradius = 2f;
    private GameObject killTarget;
    [HideInInspector] public bool canKill;

    private void OnEnable() => Player.OnPlayersLoadedInGameScene += OnGameSceneLoaded;
    private void OnDisable() => Player.OnPlayersLoadedInGameScene -= OnGameSceneLoaded;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
        playerProximity = GetComponent<PlayerProximity>();
    }

    private void OnGameSceneLoaded()
    {
        roleManager = FindObjectOfType<RoleManager>();
        StartCoroutine(GetMyRole());
    }

    IEnumerator GetMyRole()
    {
        while(myrole == null)
        {
            yield return new WaitForSeconds(0.1f);
            myrole = roleManager.GetRole(transform.name);
        }
    }

    void Update()
    {
        if (myrole == RoleManager.IMPOSTER)
        {
            if (playerProximity.NearestPlayerInside(killradius) != null)
            {
                killTarget = playerProximity.NearestPlayerInside(killradius);

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!! ATTENTION !! Not Sure if this "Player State" works !!!!!!!!
                // !!!!!! NEEDED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                if (killTarget.GetComponent<Player>().myState == Player.State.dead)
                    return;

                canKill = true;
                //Debug.Log(gameObject.name + " can kill " + killTarget);
            }
            else
                canKill = false;
        }
    }

    public void OnKill()
    {
        if (canKill)
            PV.RPC("Kill", RpcTarget.All, new string[] { gameObject.name, killTarget.name });
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, killradius);
    //}
}
