using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImposterHudManager : MonoBehaviour
{
    GameObject myplayer;

    void Start()
    {
        myplayer = PlayersList.GetMyPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickKillBtn()
    {
        myplayer.GetComponent<ImposterActions>().OnClick_KillBtn();
    }

}
