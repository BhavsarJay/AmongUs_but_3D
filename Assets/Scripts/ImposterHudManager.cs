﻿using UnityEngine;
using UnityEngine.UI;

public class ImposterHudManager : MonoBehaviour
{
    GameObject myplayer;

    KillTrigger killtrigger;
    ReportTrigger reportTrigger;

    public Button killBtn;
    public Button reportBtn;


    private void OnEnable() => Player.OnThisPlayerDead += Player_OnThisPlayerDead;

    private void OnDisable() => Player.OnThisPlayerDead -= Player_OnThisPlayerDead;

    void Start()
    {
        myplayer = PlayersList.GetMyPlayer();
        killtrigger = myplayer.GetComponent<KillTrigger>();
        reportTrigger = myplayer.GetComponent<ReportTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (killtrigger.canKill)
            killBtn.interactable = true;
        else
            killBtn.interactable = false;

        if (reportTrigger.canReport)
            reportBtn.interactable = true;
        else
            reportBtn.interactable = false;
    }

    public void OnClick_KillBtn() => myplayer.GetComponent<KillTrigger>().OnKill();

    public void OnClick_VentBtn() => myplayer.GetComponent<VentTrigger>().OnVent();

    public void OnClick_ReportBtn() => myplayer.GetComponent<ReportTrigger>().OnReport();

    private void Player_OnThisPlayerDead()
    {
        killBtn.gameObject.SetActive(false);
        reportBtn.gameObject.SetActive(false);
    }

}
