using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    GameObject nearestPlayer;
    GameObject[] players;


    private void OnEnable() => Player.OnPlayersLoadedInGameScene += OnGameSceneLoaded;
    private void OnDisable() => Player.OnPlayersLoadedInGameScene -= OnGameSceneLoaded;


    private void Start()
    {

    }

    private void OnGameSceneLoaded()
    {
        players = PlayersList.GetPlayers();
    }


    public GameObject NearestPlayerInside(float radius)
    {
        float closestDist = Mathf.Infinity;
        nearestPlayer = null;
        foreach (GameObject player in players)
        {
            if (player == this.gameObject)
                continue;

            Vector2 dist = player.transform.position - transform.position;
            if (dist.magnitude < closestDist && dist.magnitude < radius)
            {
                nearestPlayer = player;
                closestDist = dist.magnitude;
            }
        }
        return nearestPlayer;
    }
}
