using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImposterActions : MonoBehaviour
{
    
    RoleManager roleManager;
    GameObject[] players;
    PhotonView PV;
    private string myrole;
    [Range(0.5f, 5f)]
    public float killradius = 2f;
    private GameObject killTarget;
    bool canKill;
    public GameObject deadBody;

    private void OnEnable() => Player.OnPlayersLoadedInGameScene += OnGameSceneLoaded;
    private void OnDisable() => Player.OnPlayersLoadedInGameScene -= OnGameSceneLoaded;


    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnGameSceneLoaded()
    {
        roleManager = FindObjectOfType<RoleManager>();
        if (roleManager != null)
        {
            StartCoroutine(GetMyRole());
        }
        players = PlayersList.GetPlayers();
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
            if (KillTarget() != null)
            {
                canKill = true;
                Debug.Log(gameObject.name + " can kill " + killTarget);

                // Reflect in UI that you can kill.
            }
            else
            {
                canKill = false;
            }
        }
    }

    private GameObject KillTarget()
    {
        float closestDist = Mathf.Infinity;
        killTarget = null;
        foreach (GameObject player in players)
        {
            if (player == this.gameObject)
                continue;

            Vector2 dist = player.transform.position - transform.position;
            if (dist.magnitude < closestDist && dist.magnitude < killradius)
            {
                killTarget = player;
                closestDist = dist.magnitude;
            }
        }
        return killTarget;
    }

    public void OnClick_KillBtn()
    {
        print(players + " " + players.Length);
        
        if (canKill)
        {
            //Debug.Log("Killed Someone");
            transform.position = killTarget.transform.position;
            PV.RPC("Kill", RpcTarget.AllBuffered, new string[] { gameObject.name, killTarget.name });
            //Kill();
        }
    }

    [PunRPC]
    void Kill(string killersName, string targetsName)
    {
        GameObject target = PlayersList.GetPlayer(targetsName);
        GameObject killer = PlayersList.GetPlayer(killersName);

        Debug.Log("Someones Dead :(");
        // Change to ghost sprite and play animation
        Animator targetsAnimator = target.GetComponentInChildren<Animator>();
        targetsAnimator.SetTrigger("dead");

        // Spawm DeadBody
        Instantiate(deadBody, target.transform.position, Quaternion.identity);

        // Change collider to trigger
        target.GetComponent<BoxCollider2D>().enabled = false;

        // Teleport killer to targets position
        killer.transform.position = target.transform.position;
        /** Need to do somthing about this cause it doesnt teleport to new pos in others instances cause
            the transform view smoothens it out.
            
        Might help:
            https://doc.photonengine.com/en-us/pun/current/gameplay/synchronization-and-state
            https://forum.photonengine.com/discussion/13140/teleport-like-abilities-while-using-photon-transform-view
        **/
    }








    //private void OnDrawGizmos()
    //{
    //    UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, killradius);
    //}
}
