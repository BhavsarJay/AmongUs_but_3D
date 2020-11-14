using UnityEngine;
using UnityEngine.UI;

public class CrewmateHudManager : MonoBehaviour
{
    GameObject myplayer;

    ReportTrigger reportTrigger;
    public Button reportBtn;


    private void OnEnable() => Player.OnThisPlayerDead += Player_OnThisPlayerDead;

    private void OnDisable() => Player.OnThisPlayerDead -= Player_OnThisPlayerDead;

    void Start()
    {
        myplayer = PlayersList.GetMyPlayer();
        reportTrigger = myplayer.GetComponent<ReportTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reportTrigger.canReport)
            reportBtn.interactable = true;
        else
            reportBtn.interactable = false;
    }

    public void OnClick_ReportBtn() => myplayer.GetComponent<ReportTrigger>().OnReport();

    private void Player_OnThisPlayerDead()
    {
        reportBtn.gameObject.SetActive(false);
    }

}
