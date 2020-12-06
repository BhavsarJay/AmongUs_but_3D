using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayersList
{
    private const string PLAYER_ID_PREFFIX = "Player ";
    private static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private static GameObject[] playersArray;
    private static Dictionary<string, string> colors = new Dictionary<string, string>();

    public static void RegisterPlayer(string _netID, GameObject _player)
    {
        string _playerID = PLAYER_ID_PREFFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
        Debug.Log("Added Player - " + _player.name);
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
        Debug.Log("Removed Player - " + _playerID);
    }

    public static GameObject GetPlayer (string name)
    {
        return players[name];
    }

    public static GameObject GetMyPlayer()
    {
        string myName = Player.player.name;
        return GetPlayer(myName);
    }

    public static GameObject[] GetPlayers()
    {
        playersArray = players.Values.ToArray();
        return playersArray;
    }

    public static void GiveColor(string newColor, string prevColor,string playerName)
    {
        if(colors.ContainsKey(prevColor))
            colors.Remove(prevColor);
        
        colors.Add(newColor, playerName);
    }

    // Call this inside OnPlayerDisconnected and not inside OnDisable
    public static void RemoveColor(string color) => colors.Remove(color);

    public static bool IsColorAvailable(string color)
    {
        return !colors.ContainsKey(color);
    }
}
