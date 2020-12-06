using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    PhotonView PV;
    string myCurrentColor = "White";
    string[] COLORS = { "White", "Black", "Red", "Blue", "Green", "Yellow", "Brown", "Purple", "Cyan", "Pink" , "Orange", "Lime"};
    public Color[] colors;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;

        //if (!PlayerPrefs.HasKey("FavoriteColor"))
        //    PlayerPrefs.SetString("FavoriteColor", myCurrentColor);
        //else
        //    myCurrentColor = PlayerPrefs.GetString("FavoriteColor");


        StartCoroutine(GiveStartingColor());
    }

    IEnumerator GiveStartingColor()
    {
        // Wait for 3sec to get the Gice Color Rpcs of all players
        yield return new WaitForSeconds(3f);

        if (CheckForColor(myCurrentColor) == false)
            GiveNextColor();
    }

    private void GiveNextColor()
    {
        int i = 0;
        while (!CheckForColor(COLORS[i]))
        {
            i++;
            if (i >= COLORS.Length)
                return;
        }
    }

    public bool CheckForColor(string wantedColor)
    {
        if (PlayersList.IsColorAvailable(wantedColor))
        {
            //Debug.Log(wantedColor + "is available !");
            PV.RPC("GiveColor", RpcTarget.AllBuffered, new string[] { wantedColor, myCurrentColor, transform.name });
            myCurrentColor = wantedColor;
            return true;
        }
        //Debug.Log(wantedColor + "is not available.");
        return false;
    }

    [PunRPC]
    void GiveColor(string newColor, string prevColor, string playerName)
    {
        PlayersList.GiveColor(newColor, prevColor, playerName);
        //Debug.Log(newColor + "is taken by someone.");

        GameObject player = PlayersList.GetPlayer(playerName);

        int index = Array.IndexOf(COLORS, newColor);
        Color color = colors[index];

        Material BodyMat = player.GetComponentInChildren<SpriteRenderer>().material;
        BodyMat.SetColor("Color_480E891A", color);
        float h, s, v;
        Color.RGBToHSV(color,out h, out s, out v);
        Color darkColor = Color.HSVToRGB(h, s, v * 0.7f);
        BodyMat.SetColor("Color_1EA1A65B", darkColor);
    }
}
